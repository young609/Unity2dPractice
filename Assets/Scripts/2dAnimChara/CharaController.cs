using System.Collections.Generic;
using UnityEngine;

namespace _2dAnimChara
{
    public class CharaController : MonoBehaviour
    {
        [SerializeField]
        private float groundDistance = 0.01f;
        
        [SerializeField]
        private float moveSpeed = 5f;
        
        [SerializeField]
        private float jumpForce = 10f;
        
        [SerializeField]
        private int maxJumpCount = 2;
        
        [SerializeField]
        private float jumpSmoothFactor = 3f;
        
        [SerializeField]
        private LayerMask groundLayer;
        
        private static readonly int isMoveHash = Animator.StringToHash("IsMove");
        
        private static readonly int isAscendingHash = Animator.StringToHash("IsAscending");
        
        private Rigidbody2D rb;
        
        private Animator animator;
        
        private SpriteRenderer sr;

        // horizontal input
        private float h;
        
        // space bar
        private bool s;
        
        private IState state;

        private Dictionary<EState, IState> stateDict;


        private interface IState
        {
            public void Enter(CharaController c);
            public void Update(CharaController c);
            public void FixedUpdate(CharaController c);
            public void Exit(CharaController c);
        }
        
        
        private enum EState
        {
            Idle,
            Run,
            Jump,
        }
        
        
        private struct IdleState : IState
        {
            public void Enter(CharaController c)
            {
                c.animator.SetBool(isMoveHash, false);
            }

            public void Update(CharaController c)
            {
                // 입력체크
                if (c.s)
                {
                    c.ChangeState(EState.Jump);
                    return;
                }

                if (c.h == 0)
                {
                    return;
                }

                c.ChangeState(EState.Run);
            }

            public void FixedUpdate(CharaController c) { }

            public void Exit(CharaController c) { }
        }

        private struct RunState : IState
        {
            public void Enter(CharaController c)
            {
                c.animator.SetBool(isMoveHash, true);
            }

            public void Update(CharaController c)
            {
                // 입력체크
                if (c.s)
                {
                    c.ChangeState(EState.Jump);
                    return;
                }

                if (c.h == 0)
                {
                    c.ChangeState(EState.Idle);
                    return;
                }

                // 로직
                c.sr.flipX = c.h < 0;
            }

            public void FixedUpdate(CharaController c)
            {
                c.rb.linearVelocityX = c.h * c.moveSpeed;
            }

            public void Exit(CharaController c) { }
        }

        private struct JumpState : IState
        {
            private int jumpCount;
            private bool isGrounded;
            private bool isJumpRequested;
            
            public void Enter(CharaController c)
            {
                jumpCount = 0;
                isGrounded = false;
                RequestJump(c);
            }

            private void RequestJump(CharaController c)
            {
                isJumpRequested = true;
                c.animator.SetBool(isAscendingHash, true);
                jumpCount++;
            }

            public void Update(CharaController c)
            {
                // 상태전환
                if (isGrounded)
                {
                    var nextState = c.h == 0 ? EState.Idle : EState.Run;
                    c.ChangeState(nextState);
                    return;
                }

                // 다중 점프
                if (c.s && jumpCount < c.maxJumpCount)
                {
                    RequestJump(c);
                }
                else if (c.rb.linearVelocityY <= 0) // 하강 애니메이션으로 전환. RequestJump에서 IsAscending을 true로 설정하므로 겹치지 않도록 else if로 연결.
                {
                    c.animator.SetBool(isAscendingHash, false);
                }

                // Move 애니메이션 종료
                c.animator.SetBool(isMoveHash, c.h != 0);
        }

            public void FixedUpdate(CharaController c)
            {
                // 점프
                if (isJumpRequested)
                {
                    c.rb.linearVelocityY = 0f;
                    c.rb.AddForceY(c.jumpForce, ForceMode2D.Impulse);
                    isJumpRequested = false;
                }
                
                // 바닥 체크
                if (CheckGround(c, ref isGrounded))
                {
                    return;
                }

                // 점프 중의 이동, 키보드 방향에 따라 조금의 방향전환 및 가감속이 가능함.
                var sign = c.h switch
                {
                    > 0 => 1f,
                    0 => 0f,
                    < 0 => -1f,
                    _ => 0
                };

                c.rb.linearVelocityX = Mathf.Lerp(c.rb.linearVelocityX, sign * c.moveSpeed, Time.fixedDeltaTime * c.jumpSmoothFactor);
            }

            public void Exit(CharaController c)
            {
                // 안전하게 한번 더 해제
                c.animator.SetBool(isAscendingHash, false);
            }

            private static bool CheckGround(CharaController c, ref bool isGrounded)
            {
                if (isGrounded)
                {
                    return true;
                }

                // 1. 상승 체크
                if (c.rb.linearVelocityY > 0f)
                {
                    return false;
                }
                
                // 바닥 레이, 추후에 box cast, circle cast 중에 바꿔야할지도, box cast는 일관적이고, circle cast는 경사로에서도 잘 적용될 것으로 생각됨.
                var rayStartPos = c.transform.position + Vector3.down * c.groundDistance;
                var hit = Physics2D.Raycast(rayStartPos, Vector2.down, 1f, c.groundLayer);
                
                // 타입 매칭으로 체크, ray에 맞은 것 중에 유니티 fake null은 없음으로 예상. Destroy되었으면 ray에 맞지 않음.
                if (hit.collider is null)
                {
                    return false;
                }
                
                // 바닥 거리 체크
                if (hit.distance > c.groundDistance)
                {
                    return false;
                }

                isGrounded = true;
                return true;
            }
        }
        
        
        private void Start()
        {
            // 컴포넌트 획득
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            sr = GetComponent<SpriteRenderer>();
            
            // 상태 생성
            stateDict = new Dictionary<EState, IState>
            {
                { EState.Idle, new IdleState() },
                { EState.Run, new RunState() },
                { EState.Jump, new JumpState() },
            };
            
            // 최초 상태 설정
            ChangeState(EState.Idle);
        }

        private void Update()
        {
            h = Input.GetAxisRaw("Horizontal");
            s = Input.GetKeyDown(KeyCode.Space);
            
            state?.Update(this);
        }
        
        private void FixedUpdate()
        {
            state?.FixedUpdate(this);
        }
        
        private void ChangeState(EState stateEnum)
        {
            state?.Exit(this);
            state = stateDict[stateEnum];
            state?.Enter(this);
            // state?.Update(this);
        }
    }
}
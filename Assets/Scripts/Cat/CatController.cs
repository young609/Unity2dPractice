using System;
using UnityEngine;

namespace Cat
{
    public class CatController : MonoBehaviour
    {
        public event Action<float> OnMove;
        
        [SerializeField]
        private float moveSpeed = 5;
        
        [SerializeField]
        private float jumpForce = 10;
        
        [SerializeField]
        private bool isGrounded = true;
        
        [SerializeField]
        private float layDistance = 1;
        
        [SerializeField]
        private float groundDistance = 0.001f;
        
        [SerializeField]
        private int jumpCount;
        
        [SerializeField]
        private int maxJumpCount = 2;
        
        [SerializeField]
        private LayerMask groundLayer;
        
        private Rigidbody2D rb;
        private float h;
        
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            // jump check
            if (jumpCount < maxJumpCount && Input.GetKeyDown(KeyCode.Space))
            {
                rb.linearVelocityY = 0;
                rb.AddForceY(jumpForce, ForceMode2D.Impulse);
                jumpCount++;

                if (isGrounded)
                {
                    isGrounded = false;
                }
            }

            // ground check
            if (!isGrounded)
            {
                var hit = Physics2D.Raycast(transform.position, Vector2.down, layDistance, groundLayer);
                if (hit.collider && rb.linearVelocityY <= 0 && hit.distance < groundDistance)
                {
                    jumpCount = 0;

                    if (!isGrounded)
                    {
                        isGrounded = true;
                    }
                }
            }
            
            h = Input.GetAxis("Horizontal");
            if (h != 0)
            {
                OnMove?.Invoke(h * moveSpeed * Time.deltaTime);
            }
        }
    }
}

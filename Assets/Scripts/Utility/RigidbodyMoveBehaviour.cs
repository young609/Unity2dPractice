using UnityEngine;

namespace Utility
{
    public class RigidbodyMoveBehaviour : MonoBehaviour
    {
        [SerializeField]
        private float moveSpeed;

        private Rigidbody rb;
        
        private Vector3 moveDirection;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            // GetAxis
            // -1 ~ 1의 값의 변경에 보간이 들어감.
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");
        
            // GetAxisRaw
            // -1, 0, 1의 분리된 값을 반환함.
            // var h = Input.GetAxisRaw("Horizontal");
            // var v = Input.GetAxisRaw("Vertical");
            
            moveDirection = new Vector3(h, 0, v);
            moveDirection.Normalize();
        }

        private void FixedUpdate()
        {
            if (moveDirection == Vector3.zero)
            {
                return;
            }
            
            rb.MovePosition(rb.position + moveDirection * (moveSpeed * Time.fixedDeltaTime));
            rb.MoveRotation(Quaternion.LookRotation(moveDirection));
        }
    }
}
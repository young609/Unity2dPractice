using UnityEngine;

namespace Utility
{
    public class TransformMoveBehaviour : MonoBehaviour
    {
        [SerializeField]
        private float moveSpeed;
        
        private void Start()
        {
            GetComponent<Rigidbody>();
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
            
            var moveDirection = new Vector3(h, 0, v);
            moveDirection.Normalize();

            // transform으로 이동.
            transform.position += moveDirection * (Time.deltaTime * moveSpeed);
            transform.LookAt(transform.position + moveDirection);
        }
    }
}
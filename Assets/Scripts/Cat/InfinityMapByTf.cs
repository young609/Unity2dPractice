using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Cat
{
    public class InfinityMapByTf : MonoBehaviour
    {
        [SerializeField] private float scrollSpeedMultiplier;
        [SerializeField] private bool isMove;
        [SerializeField] private bool isLeft;
        [SerializeField] private float moveSpeed;

        private float tileSize;
        
        private void Start()
        {
            tileSize = GetComponent<Renderer>().bounds.size.x;
        }

        private void Update()
        {
            if (!isMove)
            {
                return;
            }

            // 순환
            switch (isLeft)
            {
                case true when tileSize < transform.position.x:
                    transform.position += Vector3.left * (tileSize * 2);
                    break;
                case false when -tileSize > transform.position.x:
                    transform.position += Vector3.right * (tileSize * 2);
                    break;
            }

            // 이동
            var moveDirection = isLeft ? Vector3.right : Vector3.left;
            transform.position += moveDirection * (moveSpeed * scrollSpeedMultiplier * Time.deltaTime);
        }
    }
}

using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Cat
{
    public class InfinityScrollByTf : MonoBehaviour
    {
        [SerializeField]
        private CatController catController;
        
        [SerializeField]
        private float scrollSpeedMultiplier;

        private float tileSize;
        
        private void Start()
        {
            tileSize = GetComponent<Renderer>().bounds.size.x;
            catController.OnMove += moveDistance =>
            {
                // 순환
                switch (moveDistance < 0)
                {
                    case true when tileSize < transform.position.x:
                        transform.position += Vector3.left * (tileSize * 2);
                        break;
                    case false when -tileSize > transform.position.x:
                        transform.position += Vector3.right * (tileSize * 2);
                        break;
                }
                
                // 이동
                transform.position += Vector3.left * (moveDistance * scrollSpeedMultiplier);
            };
        }
    }
}

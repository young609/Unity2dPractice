using UnityEngine;

namespace Turret
{
    public class BulletManager : MonoBehaviour
    {
        [SerializeField]
        private float bulletSpeed = 20f;

        private void Start()
        {
            // 이것은 컴포넌트가 삭제됨.
            // Destroy(this, 5f);
            Destroy(gameObject, 5f);
        }
    
        private void Update()
        {
            transform.position += transform.forward * (Time.deltaTime * bulletSpeed);
        }

        // transform을 사용해서 이동하면서 Collision이벤트를 통해 충돌감지를 하면 터널링이 잘 발생함. -> 사용하지 말 것.
        // Collision이벤트는 (둘 중 하나라도 rigidbody가 있음 && 둘 모두 IsTrigger가 아님.)일 때 발생함.
        private void OnCollisionEnter(Collision other)
        {
            Debug.Log($"collision with {other.gameObject.name}!");
            Destroy(gameObject);
        }
    
        // transform을 사용해서 이동하며 Trigger이벤트로 충돌을 감지하는 것이 품질이 더 좋음.
        // Trigger이벤트는 (둘 중 하나라도 rigidbody가 있음 && 둘 중 하나라도 IsTrigger를 가지고 있음)일 때 발생.
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"collision(trigger) with {other.gameObject.name}!");
            Destroy(gameObject);
        }
    }
}

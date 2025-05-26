using UnityEngine;

namespace Study
{
    public class ModifyTransform : MonoBehaviour
    {
        [SerializeField]
        private float moveSpeed;
        
        [SerializeField]
        private float rotatonSpeed;
        
        private void Update()
        {
            // 월드 기준으로 이동
            transform.position += transform.forward * (moveSpeed * Time.deltaTime);
            transform.Translate(transform.forward * (moveSpeed * Time.deltaTime), Space.World);
            
            // 로컬 기준으로 이동
            transform.localPosition += Vector3.forward * (moveSpeed * Time.deltaTime);
            transform.Translate(Vector3.forward * (moveSpeed * Time.deltaTime));
            
            
            // 월드 기준으로 회전
            var angle = rotatonSpeed * Time.deltaTime;
            var worldEuler = transform.rotation.eulerAngles;
            worldEuler.y += angle;
            transform.rotation = Quaternion.Euler(worldEuler);
            
            transform.Rotate(0, angle, 0, Space.World);
            
            
            // 로컬 기준으로 회전
            var localEuler = transform.localEulerAngles;
            localEuler.y += angle;
            transform.rotation = Quaternion.Euler(localEuler);
            
            transform.Rotate(0, angle, 0);
            
            // 특정 위치(월드 기준)의 주변을 회전
            // Rotate와 RotateAround는 시계반대방향으로 회전시킴.
            transform.RotateAround(Vector3.zero, Vector3.up, angle);
            
            // 특정 위치(월드 기준)를 바라보도록 방향을 조정
            transform.LookAt(Vector3.zero);
        }
    }
}
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    [SerializeField]
    private GameObject charaObject;

    [SerializeField]
    private float speedFactor;

    private void Update()
    {
        // Legacy InputSystem - Input Manager
        // 복잡한 설정이나 긴 코드없이 간단하게 설정이 가능하나 polling으로 구현해야한다는 단점.
        // 향후 제거될 예정.
        
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

        var charaTf = charaObject.transform;
        charaTf.position += moveDirection * (Time.deltaTime * speedFactor);
        charaTf.LookAt(charaTf.position + moveDirection);
    }   
}
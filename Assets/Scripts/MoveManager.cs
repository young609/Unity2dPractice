using UnityEngine;

public class MoveManager : MonoBehaviour
{
    [SerializeField]
    private GameObject charaObject;

    [SerializeField]
    private float speedFactor;
    
    void Update()
    {
        var moveDirection = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += Vector3.forward;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            moveDirection += Vector3.left;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveDirection += Vector3.back;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveDirection += Vector3.right;
        }
        else
        {
            return;
        }
        
        moveDirection.Normalize();
        charaObject.transform.position += moveDirection * (Time.deltaTime * speedFactor);
    }   
}
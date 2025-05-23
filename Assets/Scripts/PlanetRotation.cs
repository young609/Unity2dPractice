using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    // 자전속도
    [SerializeField]
    private float rotationSpeed = 30f;

    // 공전속도
    [SerializeField]
    private float revolutionSpeed = 100f;
    
    [SerializeField]
    private bool isRevolution;
    
    [SerializeField]
    private Transform targetPlanet;


    private void Update()
    {
        // Rotate(축의 방향 - 시계방향, degree단위의 각도)
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        if (isRevolution && targetPlanet)
        {
            // RotateAround(회전기준 - world좌표, 회전축 - 시계방향회전, degree단위의 각도)
            // 회전기준이 되는 물체의 자식 오브젝트로 붙지 않으면 안됨.
            // 회전기준이 움직였을 때, 따라서 움직여지지 않으므로 공전거리가 변하게 됨.
            transform.RotateAround(targetPlanet.position, Vector3.up, revolutionSpeed * Time.deltaTime);
        }
    }
}
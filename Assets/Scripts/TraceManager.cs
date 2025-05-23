using UnityEngine;

public class TraceManager : MonoBehaviour
{
    [SerializeField]
    private GameObject traceObject;
    
    [SerializeField]
    private Transform headTf;

    private void Update()
    {
        headTf.LookAt(traceObject.transform);
    }
}

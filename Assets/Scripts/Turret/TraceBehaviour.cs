using UnityEngine;

namespace Turret
{
    public class TraceBehaviour : MonoBehaviour
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
}

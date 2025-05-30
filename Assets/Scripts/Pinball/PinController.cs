using UnityEngine;

namespace Pinball
{
    public class PinController : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D leftBarRb;
        
        [SerializeField]
        private Rigidbody2D rightBarRb;

        [SerializeField]
        private float activeTorque;
        
        [SerializeField]
        private float inactiveTorque;
        
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                leftBarRb.AddTorque(activeTorque);
            }
            else
            {
                leftBarRb.AddTorque(inactiveTorque);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                rightBarRb.AddTorque(-activeTorque);
            }
            else
            {
                rightBarRb.AddTorque(-inactiveTorque);
            }
        }
    }
}

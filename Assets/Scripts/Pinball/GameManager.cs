using UnityEngine;

namespace Pinball
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D ballRb;
    
        private Vector3 ballSpawnPOsition;

        private void Start()
        {
            ballSpawnPOsition = ballRb.position;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            ballRb.linearVelocity = Vector3.zero;
            ballRb.position = ballSpawnPOsition;
        
            Debug.Log(ballRb.position);
        }
    }
}

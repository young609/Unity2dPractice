using UnityEngine;

namespace Door
{
    public class Coin : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Get Coin!!");
                Destroy(gameObject);
            }
        }
    }
}
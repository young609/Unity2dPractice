using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    
    [SerializeField]
    private Transform spawnPointTf;

    [SerializeField]
    private float spawnInterval;
    
    private float currentTime;

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= spawnInterval)
        {
            Instantiate(bulletPrefab, spawnPointTf.position, spawnPointTf.rotation);
            currentTime = 0;
        }
    }
}

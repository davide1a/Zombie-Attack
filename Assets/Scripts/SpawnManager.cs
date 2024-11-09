using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] zombiePrefabs;

    private float xRange = 11;
    private float zRange = 4f;
    private float zSpawnPos = 10;
    private float ySpawnPos = 1f;
    private float spawnInterval = 4;
    private float bulletSpawnInterval = 10;
    private float brideSpawnInterval = 10;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnRandomZombie", Random.Range(1, spawnInterval));
        Invoke("SpawnBullets", Random.Range(1, bulletSpawnInterval));
        Invoke("SpawnBrides", Random.Range(1, brideSpawnInterval));
    }

    void SpawnRandomZombie()
    {
        if (GameManager.SharedInstance.isGameActive)
        {
            // Generate random spawn position
            float randomX = Random.Range(-xRange, xRange);
            int randomIndex = Random.Range(0, GameManager.SharedInstance.difficultyMultiplier+1);
            Vector3 spawnPos = new Vector3(randomX, ySpawnPos, zSpawnPos);
            // Instantiate new zombie
            Instantiate(zombiePrefabs[randomIndex], spawnPos, zombiePrefabs[randomIndex].gameObject.transform.rotation);
        }
        Invoke("SpawnRandomZombie", Random.Range(1, spawnInterval) / GameManager.SharedInstance.difficultyMultiplier);
    }

    void SpawnBullets()
    {
        if (GameManager.SharedInstance.isGameActive)
        {
            // Wait for a random delay
            //yield return new WaitForSeconds(bulletSpawnInterval * GameManager.SharedInstance.difficultyMultiplier);
            // Generate rondom spawn position
            float randomX = Random.Range(-xRange, xRange);
            float randomZ = Random.Range(-zRange, zRange);
            Vector3 spawnPos = new Vector3(randomX, ySpawnPos, randomZ);
            // Get an object from the pool
            GameObject pooledCollectibles = ObjectPooler.SharedInstance.GetPooledCollectible();
            if (pooledCollectibles != null)
            {
                pooledCollectibles.SetActive(true); // Activate the new object
                pooledCollectibles.transform.position = spawnPos; // Position it at random position
            }
        }
        Invoke("SpawnBullets", Random.Range(1, bulletSpawnInterval) * GameManager.SharedInstance.difficultyMultiplier);
    }

    void SpawnBrides()
    {
        if (GameManager.SharedInstance.isGameActive)
        {
            // Generate random spawn position
            float randomX = Random.Range(-xRange, xRange);
            Vector3 spawnPos = new Vector3(randomX, ySpawnPos, zSpawnPos);
            // Get an object from the pool
            GameObject pooledBrides = ObjectPooler.SharedInstance.GetPooledBride();
            if (pooledBrides != null)
            {
                pooledBrides.SetActive(true); // Activate the new object
                pooledBrides.transform.position = spawnPos; // Position it at random position
            }
        }
        Invoke("SpawnBrides", Random.Range(1, brideSpawnInterval) * GameManager.SharedInstance.difficultyMultiplier);
    }
}

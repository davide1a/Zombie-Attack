using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] zombies;
    [SerializeField] private GameObject bullets;

    private float xRange = 11;
    private float zRange = 5f;
    private float zSpawnPos = 10;
    private float ySpawnPos = 0.5f;

    private int startDelay = 2;
    private GameManager gameManager;

    //private int spawnInterval = 3;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        Invoke("SpawnRandomZombie", startDelay);
        Invoke("SpawnBullets", startDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnRandomZombie()
    {
        float randomX = Random.Range(-xRange, xRange);
        int randomIndex = Random.Range(0, zombies.Length);

        Vector3 spawnPos = new Vector3(randomX, ySpawnPos, zSpawnPos);

        Instantiate(zombies[randomIndex], spawnPos, zombies[randomIndex].gameObject.transform.rotation);

        Invoke("SpawnRandomZombie", Random.Range(1, 5));
    }

    void SpawnBullets()
    {
        float randomX = Random.Range(-xRange, xRange);
        float randomZ = Random.Range(-zRange, zRange);

        Vector3 spawnPos = new Vector3(randomX, ySpawnPos, randomZ);

        Instantiate(bullets, spawnPos, bullets.gameObject.transform.rotation);

        Invoke("SpawnBullets", Random.Range(5, 10));
    }
}

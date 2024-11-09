using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bulletCountText;
    public GameObject titleScreen;
    public GameObject gameOverScreen;
    public Button restartButton;
    public bool isGameActive;
    private int score;
    public int bulletCount;
    public int difficultyMultiplier;
    [SerializeField] private GameObject[] zombies;
    [SerializeField] private GameObject bulletCollectible;

    private float xRange = 11;
    private float zRange = 5f;
    private float zSpawnPos = 10;
    private float ySpawnPos = 0.5f;

    //private int startDelay = 2;
    private float spawnInterval = 2;
    private int bulletSpawnInterval = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameOver()
    {
        gameOverScreen.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateBullets(int bulletsToAdd)
    {
        bulletCount += bulletsToAdd;
        bulletCountText.text = "Ammo: " + bulletCount;
        Debug.Log("Bullet Count: " + bulletCount);
    }

    public void StartGame(int difficulty)
    {
        difficultyMultiplier = difficulty;
        isGameActive = true;
        score = 0;
        UpdateScore(0);
        titleScreen.SetActive(false);
        StartCoroutine(SpawnRandomZombie());
        StartCoroutine(SpawnBullets());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator SpawnRandomZombie()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnInterval / difficultyMultiplier);
            float randomX = Random.Range(-xRange, xRange);
            int randomIndex = Random.Range(0, difficultyMultiplier + 2);

            Vector3 spawnPos = new Vector3(randomX, ySpawnPos, zSpawnPos);

            Instantiate(zombies[randomIndex], spawnPos, zombies[randomIndex].gameObject.transform.rotation);
        }
    }

    IEnumerator SpawnBullets()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(bulletSpawnInterval * difficultyMultiplier);
            float randomX = Random.Range(-xRange, xRange);
            float randomZ = Random.Range(-zRange, zRange);

            Vector3 spawnPos = new Vector3(randomX, ySpawnPos, randomZ);

            Instantiate(bulletCollectible, spawnPos, bulletCollectible.gameObject.transform.rotation);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager SharedInstance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bulletCountText;
    public GameObject titleScreen;
    public GameObject gameOverScreen;
    public Button restartButton;
    public bool isGameActive;
    private int score;
    public int bulletCount;
    public int difficultyMultiplier = 1;

    void Awake()
    {
        SharedInstance = this;
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
    }

    public void StartGame(int difficulty)
    {
        difficultyMultiplier = difficulty;
        isGameActive = true;
        score = 0;
        UpdateScore(0);
        titleScreen.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

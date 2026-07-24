using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int score;
    private bool isGameOver;

    private UIManager uiManager;
    private SpawnManager spawnManager;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        Target.OnTargetHit += AddScore;
        Target.OnGameOver += GameOver;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        Target.OnTargetHit -= AddScore;
        Target.OnGameOver -= GameOver;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeSceneObjects();
    }

    void InitializeSceneObjects()
    {
        uiManager = FindAnyObjectByType<UIManager>();
        spawnManager = FindAnyObjectByType<SpawnManager>();

        InitializeGameState();
    }

    void InitializeGameState()
    {
        score = 0;
        isGameOver = false;

        if (uiManager != null)
        {
            uiManager.UpdateScore(score);
        }
    }

    public void StartGame(int difficulty)
    {
        GameObject titleScreen = GameObject.FindGameObjectWithTag("TitleScreen");

        if(titleScreen != null)
        {
            titleScreen.SetActive(false);
        }

        if (spawnManager != null)
        {
            spawnManager.StartSpawning();
            spawnManager.spawnRate /= difficulty;
        }
    }

    private void AddScore(int amount)
    {
        if (isGameOver)
            return;

        score += amount;
        uiManager.UpdateScore(score);
    }

    private void GameOver()
    {
        isGameOver = true;

        if (uiManager != null)
        {
            uiManager.ShowGameOver();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

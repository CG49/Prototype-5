using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int score;
    private int lives;
    private bool isPaused;
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

        GameEvents.OnGameEvent += HandleGameEvent;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        GameEvents.OnGameEvent -= HandleGameEvent;
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
        lives = 3;
        isGameOver = false;

        InputManager.Instance.DisableGameplay();

        if (uiManager != null)
        {
            uiManager.UpdateScore(score);
            uiManager.UpdateLives(lives);
        }
    }

    public void StartGame(int difficulty)
    {
        GameObject titleScreen = GameObject.FindGameObjectWithTag("TitleScreen");

        if(titleScreen != null)
            titleScreen.SetActive(false);

        InputManager.Instance.EnableGameplay();

        if (spawnManager != null)
        {
            spawnManager.StartSpawning();
            spawnManager.spawnRate /= difficulty;
        }
    }

    private void HandleGameEvent(GameEvent gameEvent)
    {
        switch (gameEvent.Type)
        {
            case GameEventType.Score:
                AddScore(gameEvent.Value);
                break;

            case GameEventType.Lives:
                AddLives(gameEvent.Value);
                break;

            case GameEventType.GameOver:
                GameOver();
                break;

            case GameEventType.Pause:
                TogglePause();
                break;
        }
    }

    private void AddScore(int amount)
    {
        if (isGameOver)
            return;

        if (uiManager != null)
        {
            score += amount;
            uiManager.UpdateScore(score);
        }
    }

    private void AddLives(int value)
    {
        if (isGameOver)
            return;

        if (uiManager != null)
        {
            lives = Mathf.Max(0, lives + value);
            uiManager.UpdateLives(lives);

            if (lives <= 0)
                GameEvents.Raise(new GameEvent(GameEventType.GameOver));
        }
    }

    private void GameOver()
    {
        isGameOver = true;

        if (uiManager != null)
            uiManager.ShowGameOver();
    }

    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
            InputManager.Instance.DisableGameplay();
        else
            InputManager.Instance.EnableGameplay();

        if (uiManager != null)
            uiManager.ShowPauseScreen(isPaused);

        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

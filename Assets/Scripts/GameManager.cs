using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    private int score;
    private bool isGameOver;

    void Awake()
    {
        score = 0;
        uiManager.UpdateScore(score);
    }

    void OnEnable()
    {
        Target.OnTargetHit += AddScore;
        Target.OnGameOver += GameOver;
    }

    void OnDisable()
    {
        Target.OnTargetHit -= AddScore;
        Target.OnGameOver -= GameOver;
    }

    private void AddScore(int amount)
    {
        if (!isGameOver) {
            score += amount;
            uiManager.UpdateScore(score);
        }
    }

    private void GameOver()
    {
        isGameOver = true;

        uiManager.ShowGameOver();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

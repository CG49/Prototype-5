using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject pausePanel;

    public void UpdateScore(int score)
    {
        scoreText.text = "Score : " + score;
    }

    public void UpdateLives(int value)
    {
        livesText.text = "Lives : " + value;
    }

    public void ShowGameOver() {
        gameOverText.gameObject.SetActive(true);
        ShowRestartButton();
    }

    private void ShowRestartButton()
    {
        restartButton.gameObject.SetActive(true);
    }

    public void ShowPauseScreen(bool show)
    {
        pausePanel.SetActive(show);
    }
}

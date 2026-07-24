using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button restartButton;

    public void UpdateScore(int score)
    {
        scoreText.text = "Score : " + score;
    }

    public void ShowGameOver() {
        gameOverText.gameObject.SetActive(true);
        ShowRestartButton();
    }

    private void ShowRestartButton()
    {
        restartButton.gameObject.SetActive(true);
    }
}

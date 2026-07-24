using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    private int score;

    void Awake()
    {
        score = 0;
        uiManager.UpdateScore(score);
    }

    void OnEnable()
    {
        Target.OnTargetHit += AddScore;
    }

    void OnDisable()
    {
        Target.OnTargetHit -= AddScore;
    }

    private void AddScore(int amount)
    {
        score += amount;
        uiManager.UpdateScore(score);
    }
}

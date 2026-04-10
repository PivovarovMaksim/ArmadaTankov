using UnityEngine;
using TMPro;

public class ScoreListener : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    
    [Header("Settings")]
    public bool isHighScore = false;
    
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        
        if (scoreText == null)
        {
            return;
        }
        
        // Подписываемся на нужные события
        if (isHighScore)
        {
            ScoreManager.OnHighScoreChanged += UpdateScore;
        }
        else
        {
            ScoreManager.OnScoreChanged += UpdateScore;
        }
        
        // Начальное обновление
        if (ScoreManager.Instance != null)
        {
            if (isHighScore)
            {
                UpdateScore(ScoreManager.Instance.GetHighScore());
            }
            else
            {
                UpdateScore(ScoreManager.Instance.GetCurrentScore());
            }
        }
    }
    
    void UpdateScore(int newScore)
    {
        if (scoreText != null)
        {
            if (isHighScore)
                scoreText.text = "Рекорд: " + newScore;
            else
                scoreText.text = "Счет: " + newScore;
        }
    }
    
    void OnDestroy()
    {
        // Отписываемся от событий
        if (isHighScore)
        {
            ScoreManager.OnHighScoreChanged -= UpdateScore;
        }
        else
        {
            ScoreManager.OnScoreChanged -= UpdateScore;
        }
    }
}

using UnityEngine;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    
    // События для обновления UI
    public static event Action<int> OnScoreChanged;
    public static event Action<int> OnHighScoreChanged;
    
    private int currentScore = 0;
    private int highScore = 0;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        LoadHighScore();
    }
    
    public void AddScore(int points)
    {
        currentScore += points;
        
        // Отправляем событие об изменении текущего счета
        OnScoreChanged?.Invoke(currentScore);
        
        // Проверяем рекорд
        if (currentScore > highScore)
        {
            highScore = currentScore;
            SaveHighScore();
            OnHighScoreChanged?.Invoke(highScore);
        }
    }
    
    public void ResetScore()
    {
        currentScore = 0;
        OnScoreChanged?.Invoke(currentScore);
    }
    
    void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }
    
    void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }
    
    public int GetCurrentScore() { return currentScore; }
    public int GetHighScore() { return highScore; }
}

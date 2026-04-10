using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class TankSpawner : MonoBehaviour
{
    [Header("Tank Settings")]
    [SerializeField] private GameObject tankPrefab;
    [SerializeField] private int maxTanks = 5;
    [SerializeField] private float checkInterval = 1f;
    
    [Header("Spawn Area")]
    [SerializeField] private float arenaWidth = 50f;
    [SerializeField] private float arenaHeight = 50f;
    [SerializeField] private float yPosition = 0f;
    
    [Header("UI Button")]
    [SerializeField] private UnityEngine.UI.Button exitButton;
    
    void Start()
    {
        // Создаем начальные 5 танков
        for (int i = 0; i < maxTanks; i++)
        {
            SpawnTank();
        }
        
        // Запускаем периодическую проверку
        InvokeRepeating("CheckAndSpawn", 0.5f, checkInterval);
        
        // Привязываем кнопку выхода
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitToMenu);
        }
    }
    
    void CheckAndSpawn()
    {
        // Считаем текущие танки на сцене
        GameObject[] tanks = GameObject.FindGameObjectsWithTag("Enemy");
        int currentCount = tanks.Length;
        
        // Если танков меньше 5, создаем недостающие
        if (currentCount < maxTanks)
        {
            int neededTanks = maxTanks - currentCount;
            for (int i = 0; i < neededTanks; i++)
            {
                SpawnTank();
            }
        }
    }
    
    void SpawnTank()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(-arenaWidth / 2, arenaWidth / 2),
            yPosition,
            Random.Range(-arenaHeight / 2, arenaHeight / 2)
        );
        
        Instantiate(tankPrefab, randomPosition, Quaternion.identity);
    }
    
    void ExitToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuCamera : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Transform targetTank;
    [SerializeField] private float distance = 3f;
    [SerializeField] private float height = 2f;
    [SerializeField] private float rotationSpeed = 20f;
    
    [Header("UI Buttons")]
    [SerializeField] private Button startButton;  // СЮДА ПЕРЕТАСКИВАЕШЬ КНОПКУ START
    [SerializeField] private Button quitButton;   // СЮДА ПЕРЕТАСКИВАЕШЬ КНОПКУ QUIT
    
    private float currentAngle = 0f;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        // ПРИВЯЗКА КНОПОК ЧЕРЕЗ КОД
        if (startButton != null)
            startButton.onClick.AddListener(StartGame);
        
        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
    }
    
    void Update()
    {
        // Вращение камеры
        currentAngle += rotationSpeed * Time.deltaTime;
        
        float rad = currentAngle * Mathf.Deg2Rad;
        float x = Mathf.Sin(rad) * distance;
        float z = Mathf.Cos(rad) * distance;
        
        transform.position = new Vector3(
            targetTank.position.x + x,
            targetTank.position.y + height,
            targetTank.position.z + z
        );
        
        transform.LookAt(targetTank);
    }
    
    void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    
    void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}

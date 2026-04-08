using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private float lifeTime = 3f; // Время жизни снаряда
    [SerializeField] private float speed = 20f; // Скорость полета
    
    private Rigidbody rb3d;
    private Vector3 startPosition;
    
    void Start()
    {
        startPosition = transform.position;
        
        // Получаем компонент Rigidbody (поддержка 2D и 3D)
        rb3d = GetComponent<Rigidbody>();
        
        // Устанавливаем скорость
        SetVelocity();
        
        // Автоматическое уничтожение через время
        Destroy(gameObject, lifeTime);
    }
    
    void SetVelocity()
    {
        // Для 3D
        if (rb3d != null)
        {
            rb3d.linearVelocity = transform.right * speed;
        }
    }
    
    bool CanInteractWithObject(GameObject obj)
    {
        // Проверяем тег стены
        if (obj.CompareTag("Obstacle"))
        {
            return true; // Со стенами взаимодействуем
        }
        
        // Проверяем, является ли объект игроком (если нужно)
        if (obj.CompareTag("Player"))
        {
            return false;
        }
        
        // С остальными объектами не взаимодействуем
        return false;
    }
}

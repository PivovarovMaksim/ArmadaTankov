using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private JoystickController joystick;
    [SerializeField] private Transform turret;
    [SerializeField] private Transform firePoint; // Пустой объект на конце пушки
    [SerializeField] private GameObject bulletPrefab;
    
    [Header("Turret Settings")]
    [SerializeField] private float rotationSpeed = 5f;
    
    [Header("Shooting Settings")]
    [SerializeField] private float fireDelay = 0.5f;
    
    private float fireTimer = 0f;
    
    void Start()
    {
        fireTimer = 0f;
    }
    
    void Update()
    {
        if (joystick == null) return;
        
        float horizontal = joystick.Horizontal();
        float vertical = joystick.Vertical();
        
        RotateTurret(horizontal, vertical);
        HandleShooting();
    }
    
    void RotateTurret(float horizontal, float vertical)
    {
        if (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)
        {
            float angle = Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, -angle + 90f, 0f);
            turret.rotation = Quaternion.Slerp(turret.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
    
    void HandleShooting()
    {
        if (fireTimer > 0)
        {
            fireTimer -= Time.deltaTime;
        }
        
        if (fireTimer <= 0)
        {
            Shoot();
            fireTimer = fireDelay;
        }
    }
    
    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;
        
        // Создаём снаряд в позиции и с поворотом firePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        
        // Открепляем от башни/танка
        bullet.transform.parent = null;
        
        // Передаём владельца (чтобы не сталкивался с игроком)
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            // Находим игрока (владельца)
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            bulletScript.SetOwner(player);
        }
        
        // Уничтожаем через 3 секунды
        Destroy(bullet, 3f);
    }
}

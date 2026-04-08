using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour
{
    [Header("References")]
    private VariableJoystick joystick; // Ссылка на джойстик
    [SerializeField] private Transform turret; // Дуло/башня танка
    [SerializeField] private Transform firePoint; // Точка вылета снаряда
    [SerializeField] private GameObject bulletPrefab; // Префаб снаряда
    
    [Header("Turret Settings")]
    [SerializeField] private float rotationSpeed = 5f; // Скорость поворота дула
    
    [Header("Shooting Settings")]
    [SerializeField] private float fireDelay = 0.5f; // Задержка между выстрелами
    
    private float fireTimer = 0f;
    
    void Start()
    {
        fireTimer = 0f;
        joystick = GameObject.Find("Joystick1").GetComponent<VariableJoystick>();
    }
    
    void Update()
    {
        if (joystick == null) return;
        
        // Получаем ввод с джойстика
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
        
        // Управление поворотом башни
        RotateTurret(horizontal, vertical);
        
        // Стрельба
        //HandleShooting();
    }
    
    void RotateTurret(float horizontal, float vertical)
    {
        // Если джойстик не в центре
        if (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)
        {
            // Вычисляем угол направления
            float angle = Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg;
            
            // Создаем целевое вращение
            Quaternion targetRotation = Quaternion.Euler(0f, -angle + 90f, 0f);
            
            // Плавно поворачиваем дуло
            turret.rotation = Quaternion.Slerp(turret.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
    
    void HandleShooting()
    {
        // Таймер стрельбы
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
        // Создаем снаряд
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Уничтожаем снаряд через 3 секунды, чтобы не засорять сцену
        Destroy(bullet, 5f);
    }
}

using UnityEngine;

public class EnemyTankAI : MonoBehaviour
{
    private Rigidbody body;
    public float speed = 3f;
    public float changeDirectionInterval = 3f;
    public float detectionDistance = 1f;
    private Vector3 direction;
    private float timer;
    
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    
    private float fireTimer = 0f;
    private float fireDelay = 2f;
    
    void Start()
    {
        fireTimer = 0f;
        body = GetComponent<Rigidbody>();
        ChooseRandomDirection();
        timer = changeDirectionInterval;
    }

    void FixedUpdate()
    {
        // Обновляем таймер
        timer -= Time.fixedDeltaTime;

        // Если пришло время сменить направление или столкновение обнаружено
        if (timer <= 0 || IsObstacleAhead())
        {
            ChooseRandomDirection();
            timer = changeDirectionInterval;
        }

        // Перемещаемся через MovePosition
        Vector3 newPosition = body.position + direction * speed * Time.fixedDeltaTime;
        body.MovePosition(newPosition);

        // Мгновенный поворот в сторону движения
        if (direction != Vector3.zero)
        {
            transform.forward = direction;
        }
        
        HandleShooting();
    }

    void ChooseRandomDirection()
    {
        // Выбираем случайное направление, исключая текущее (опционально)
        Vector3[] directions = new Vector3[]
        {
            Vector3.forward,
            Vector3.right,
            Vector3.back,
            Vector3.left,
        };

        Vector3 newDirection;
        do
        {
            newDirection = directions[Random.Range(0, directions.Length)];
        }
        while (newDirection == direction); // Чтобы не выбирать то же самое направление
        
        direction = newDirection;
    }

    bool IsObstacleAhead()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, detectionDistance))
        {
            if (hit.collider.CompareTag("Obstacle") || 
                hit.collider.CompareTag("Player") || 
                hit.collider.CompareTag("Enemy"))
            {
                return true;
            }
        }
        return false;
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
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Destroy(bullet, 5f);
    }
}

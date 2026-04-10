using UnityEngine;

public class DummyLogic : MonoBehaviour
{
    private Rigidbody body;
    public float speed = 3f;
    public float changeDirectionInterval = 3f;
    public float detectionDistance = 1f;
    private float timer;
    
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    
    private float fireTimer = 0f;
    private float fireDelay = 2f;
    
    void Start()
    {
        fireTimer = 0f;
        body = GetComponent<Rigidbody>();
        timer = changeDirectionInterval;
    }

    void FixedUpdate()
    {
        HandleShooting();
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

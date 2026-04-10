using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private float speed = 20f;
    
    private Vector3 shootDirection;
    private GameObject owner;
    
    void Start()
    {
        // Если направление не задано вручную, используем transform.forward
        if (shootDirection == Vector3.zero)
            shootDirection = transform.forward;
            
        Destroy(gameObject, lifeTime);
    }
    
    void Update()
    {
        transform.Translate(shootDirection * speed * Time.deltaTime, Space.World);
    }
    
    public void SetDirection(Vector3 direction)
    {
        shootDirection = direction.normalized;
        // Поворачиваем пулю в направлении полета
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction);
    }
    
    public void SetOwner(GameObject ownerObject)
    {
        owner = ownerObject;
        
        Collider bulletCollider = GetComponent<Collider>();
        Collider ownerCollider = owner.GetComponent<Collider>();
        
        if (bulletCollider != null && ownerCollider != null)
        {
            Physics.IgnoreCollision(bulletCollider, ownerCollider);
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == owner) return;
        
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
                player.TakeDamage();
            
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}

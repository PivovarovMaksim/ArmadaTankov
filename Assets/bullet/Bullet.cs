using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private float speed = 20f;
    
    private Vector3 shootDirection;
    private GameObject owner;
    
    public ParticleSystem explosionEffect;
    
    void Start()
    {
        shootDirection = transform.forward;
        Destroy(gameObject, lifeTime);
    }
    
    void Update()
    {
        transform.Translate(shootDirection * speed * Time.deltaTime, Space.World);
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
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Взрыв эффект
            if (explosionEffect != null)
            {
                ParticleSystem explosion = Instantiate(explosionEffect, collision.gameObject.transform.position, Quaternion.identity);
                explosion.Play();
                Destroy(explosion.gameObject, explosion.main.duration);
            }
            
            // ПРИБАВЛЯЕМ ОЧКИ через ScoreManager
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(100);
            }
            else
            {
                Debug.LogError("ScoreManager не найден на сцене!");
            }
            
            // Уничтожаем врага и пулю
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}

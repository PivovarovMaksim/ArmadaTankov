using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public JoystickController joystick;
    
    private Rigidbody player_body;
    private Vector3 direction;
    private Vector3 last_direction;
    private bool is_rotated = false;
    
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI gameOverText;
    
    public int health = 3;
    public float speed = 30f;
    public float rotation_speed = 8f;
    public float rotation_complete_threshold = 5f;
    
    [SerializeField] private ParticleSystem smokeEffect;
    
    void Start()
    {
        player_body = GetComponent<Rigidbody>();
        last_direction = Vector3.forward;
    }
    
    void Update()
    {
        // Управление
        float horizontal = joystick.Horizontal();
        float vertical = joystick.Vertical();
        
        if (horizontal > 0.2f) horizontal = 1;
        else if (horizontal < -0.2f) horizontal = -1;
        else horizontal = 0;
        
        if (vertical > 0.2f) vertical = 1;
        else if (vertical < -0.2f) vertical = -1;
        else vertical = 0;
        
        Vector3 input_direction = new Vector3(horizontal, 0f, vertical);
        
        if (input_direction.magnitude > 0.1f)
        {
            direction = input_direction.normalized;
            last_direction = direction;
            is_rotated = false;
        }
        else
        {
            direction = Vector3.zero;
        }
    }
    
    void Move()
    {
        if (direction.magnitude > 0.1f && is_rotated)
        {
            player_body.linearVelocity = direction * speed * Time.fixedDeltaTime;
        }
        else
        {
            player_body.linearVelocity = Vector3.zero;
        }
    }
    
    void Rotate()
    {
        if (last_direction.magnitude > 0.1f)
        {
            Quaternion target_rotation = Quaternion.LookRotation(last_direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target_rotation, 
                rotation_speed * Time.fixedDeltaTime);
            
            float angle_to_target = Quaternion.Angle(transform.rotation, target_rotation);
            if (angle_to_target < rotation_complete_threshold)
            {
                is_rotated = true;
            }
        }
    }
    
    void FixedUpdate()
    {
        Rotate();
        Move();
    }
    
    public void TakeDamage()
    {
        health -= 1;
        
        healthText.text = "Жизни: " + health;
        
        if (health < 2)
        {
            smokeEffect.Play();
        }
        
        if (health <= 0)
        {
            Destroy(gameObject);
            gameOverText.text = "ИГРА ОКОНЧЕНА";
        }
    }
}

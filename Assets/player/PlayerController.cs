using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public JoystickController joystick;
    private VariableJoystick joystick;

    private Rigidbody player_body;
    private Vector3 direction;
    private Vector3 last_direction;
    private bool is_rotated = false; // Флаг, что поворот завершен
    
    public float speed = 300f;
    public float rotation_speed = 800f;
    public float rotation_complete_threshold = 5f; // Порог завершения поворота (в градусах)
    
    

    void Start()
    {
        player_body = GetComponent<Rigidbody>();
        last_direction = Vector3.forward;
        joystick = GameObject.Find("Joystick0").GetComponent<VariableJoystick>();
        
    }

    void Update()
    {
        //float horizontal = joystick.Horizontal();
        //float vertical = joystick.Vertical();
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
        //Debug.Log(horizontal + ", " + vertical);
        
        // фиксированный набор направлений (ограничиваем 360 градусов на 8 направлений)
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
            is_rotated = false; // Как только получили новое направление, сбрасываем флаг
        }
        else
        {
            direction = Vector3.zero;
        }
    }

    void Move()
    {
        // Двигаемся только если есть направление И поворот завершен
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
            
            // Проверяем, завершен ли поворот
            float angle_to_target = Quaternion.Angle(transform.rotation, target_rotation);
            if (angle_to_target < rotation_complete_threshold)
            {
                is_rotated = true; // Поворот завершен, можно двигаться
            }
        }
    }

    void FixedUpdate()
    {
        Rotate();
        Move();
    }

    
}

using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Player : Mortal
{
    int anim_moving = Animator.StringToHash("moving");
    int anim_direction = Animator.StringToHash("direction");
    [SerializeField] GameObject bubble_linear, bubble_quadratic;
    [SerializeField] GameObject flag_go;
    [SerializeField] float Q_cooldown = 0.8f;
    [SerializeField] float W_cooldown = 2.0f;

    [SerializeField] GameObject hint_w_splash, hint_w_reach;
    [SerializeField] PlayerBar player_health_bar;

    float Q_time = 0.0f;
    bool Q_attacking = false;

    float W_time = 0.0f;
    bool W_pressing = false;
    Vector2 W_pos;

    public float speed = 1.0f;
    Vector2 velocity = Vector2.zero;

    bool has_moveto = false;
    Vector2 moveto;

    void FixedUpdate()
    {
        if (!active) return;

        if (has_moveto)
        {
            Vector2 direction = moveto - (Vector2)transform.position;
            float distance = direction.magnitude;
            if (distance < 0.1f)
            {
                has_moveto = false;
                velocity = Vector2.zero;
                flag_go.SetActive(false);
            }
            else
            {
                // TODO: pathfind
                velocity = direction.normalized;
            }
        }

        transform.Translate(velocity * Time.deltaTime * speed);
    }

    void Update()
    {
        if (!active) return;

        Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float mouse_distance = (mouse_pos - (Vector2)transform.position).magnitude;

        if (Q_time > 0)
        {
            Q_time -= Time.deltaTime;
        }
        else if (Q_attacking)
        {
            // shoot small bubble
            GameObject new_bubble = Instantiate(bubble_linear, transform.position, Quaternion.identity);

            Vector2 target_position = mouse_pos;
            var attack_direction = mouse_pos - (Vector2)transform.position;
            new_bubble.GetComponent<BubbleLinear>().Init(attack_direction, 50, 1.0f, 5.0f);

            Q_time = Q_cooldown;
        }

        // W skill
        if (W_time > 0)
        {
            W_time -= Time.deltaTime;
        }
        else if (W_pressing)
        {
            if (mouse_distance > 8.0f)
            {
                // skill reach
                W_pos = (Vector2)(transform.position) + (mouse_pos - (Vector2)(transform.position)).normalized * 8.0f;
            }
            else
            {
                W_pos = mouse_pos;
            }
            hint_w_reach.transform.position = transform.position;
            hint_w_reach.SetActive(true);
            hint_w_splash.transform.position = W_pos;
            hint_w_splash.SetActive(true);
        }

        if(!W_pressing)
        {
            hint_w_reach.SetActive(false);
            hint_w_splash.SetActive(false);
        }

        // Movement animation
        if (velocity != Vector2.zero)
        {
            // Moving
            animator.SetBool(anim_moving, true);
            if (velocity.x < 0)
            {
                animator.SetInteger(anim_direction, 1);
            }
            else if (velocity.x > 0)
            {
                animator.SetInteger(anim_direction, 2);
            }
            else
            {
                animator.SetInteger(anim_direction, 0);
            }
        }
        else
        {
            // Not moving
            animator.SetBool(anim_moving, false);
        }
    }

    public void MoveTo(InputAction.CallbackContext cc)
    {
        if (!active) return;
        if (cc.phase == InputActionPhase.Started)
        {
            has_moveto = true;
            moveto = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            flag_go.SetActive(false);
            flag_go.transform.position = moveto;
            flag_go.SetActive(true);
        }
    }

    public void Move(InputAction.CallbackContext cc)
    {
        if (!active) return;
        if (cc.phase == InputActionPhase.Started)
        {
            has_moveto = false;
            velocity = cc.ReadValue<Vector2>();
        }
    }

    public void Stop(InputAction.CallbackContext cc)
    {
        if (!active) return;
        if (cc.phase == InputActionPhase.Started)
        {
            has_moveto = false;
            velocity = Vector2.zero;
            flag_go.SetActive(false);

        }
    }

    public void Skill_Q(InputAction.CallbackContext cc)
    {
        if (!active) return;
        if (cc.phase == InputActionPhase.Started)
        {
            Q_attacking = true;
        }
        else if(cc.phase == InputActionPhase.Canceled)
        {
            Q_attacking = false;
        }
    }

    public void Skill_W(InputAction.CallbackContext cc)
    {
        if (!active) return;
        if (cc.phase == InputActionPhase.Started)
        {
            W_pressing = true;
        }
        else if (cc.phase == InputActionPhase.Canceled)
        {
            W_pressing = false;
            
            if(W_time <= 0)
            {
                // shoot skill
                GameObject new_bubble = Instantiate(bubble_quadratic, transform.position, Quaternion.identity);
                var target_position = W_pos;
                new_bubble.GetComponent<BubbleQuadratic>().Init(target_position, 20, 1.0f);

                W_time = W_cooldown;
            }
        }
    }

    protected override void OnDamage()
    {
        player_health_bar.set_rate((float)health / (float)max_health);
    }
}

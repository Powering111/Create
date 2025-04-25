using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Player : Mortal
{
    int anim_moving = Animator.StringToHash("moving");
    int anim_direction = Animator.StringToHash("direction");
    [SerializeField] GameObject bubble_linear;
    [SerializeField] GameObject flag_go;
    [SerializeField] float Q_cooldown = 0.8f;

    [SerializeField] PlayerBar player_health_bar;

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


    protected override void OnDamage()
    {
        player_health_bar.set_rate((float)health / (float)max_health);
    }
}

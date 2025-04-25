using UnityEditor.UI;
using UnityEngine;

public class MagmaStone : Enemy
{
    [SerializeField] float movement_speed = 1.0f;

    float can_move_time = 0.0f;
    float stop_time = 1.5f;

    Vector2 direction;
    protected override void Recognize(GameObject target)
    {

        if(can_move_time > 0)
        {

            rb.MovePosition(rb.position + direction * movement_speed * Time.deltaTime);

            can_move_time -= Time.deltaTime;
            if(can_move_time <= 0)
            {
                stop_time = 0.8f;
            }
        }
        else
        {
            stop_time -= Time.deltaTime;
            if(stop_time <= 0.0f)
            {
                Vector2 delta = target.transform.position - transform.position;
                direction = delta.normalized;
                can_move_time = 1.0f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (active)
        {
            if (collision.gameObject.layer == 6)
            {
                // collided with player
                Mortal component;
                if (collision.gameObject.TryGetComponent<Mortal>(out component))
                {
                    component.damage(10);
                    Pop();
                }
            }
        }
    }
}

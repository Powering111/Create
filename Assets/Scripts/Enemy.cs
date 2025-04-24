using UnityEngine;

public class Enemy : Mortal
{
    [SerializeField] EnemyBar health_bar;
    [SerializeField] float speed = 1.0f;
    [SerializeField] float move_range = 2.0f;
    [SerializeField] float recognition_range = 10.0f;

    GameObject player;
    public void Init(GameObject Player)
    {
        player = Player;
    }

    private void FixedUpdate()
    {
        if (active)
        {
            // move
            Vector2 delta = player.transform.position - transform.position;
            float distance = delta.magnitude;
            if(distance < recognition_range && distance >= move_range) { 
                Vector2 direction = delta.normalized;
                Vector2 translation = direction.normalized * speed * Time.deltaTime;
                transform.Translate(translation);
            }
            else if(distance < move_range - 0.05f)
            {
                Vector2 direction = -delta.normalized;
                Vector2 translation = direction.normalized * speed * Time.deltaTime;
                transform.Translate(translation);
            }
        }
    }

    override protected void OnDamage()
    {
        health_bar.set_rate((float)health / (float)max_health);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active)
        {
            if(collision.gameObject.layer == 6)
            {
                // collided with player
                Player component;
                if(collision.gameObject.TryGetComponent<Player>(out component))
                {
                    component.damage(10);
                }
            }
        }
    }
}

using UnityEngine;

public class BubbleLinear : Projectile
{
    [SerializeField] float speed;
    Vector2 direction;
    float lifetime;

    public void Init(Vector2 direction, int power = 10, float lifetime = 1.0f, float speed = 5.0f)
    {
        base.Init(power);
        this.direction = direction.normalized;
        this.speed = speed;

        this.lifetime = lifetime;
    }

    private void FixedUpdate()
    {
        if (active)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void Update()
    {
        if (lifetime < 0)
        {
            Pop();
        }
        else
        {
            lifetime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active && collidable && collision.gameObject.layer == 7)
        {
            Damage(collision.gameObject);
            Pop();
        }
    }
}

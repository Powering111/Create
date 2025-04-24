using UnityEngine;

public class BubbleQuadratic : Projectile
{
    [SerializeField] float shoot_time;
    float time = 0.0f;

    const float max_height = 1.0f;
    float height = 0.0f;
    float radius = 1.0f;
    float m;

    Vector2 direction;
    Vector2 start_pos, target_pos, real_pos;
    public void Init(Vector2 target_pos, int power = 10, float shoot_time = 2.0f, float radius = 1.0f)
    {
        this.start_pos = transform.position;
        this.target_pos = target_pos;
        this.radius = radius;

        this.real_pos = start_pos;
        Vector2 dir = target_pos - start_pos;
        this.m = dir.magnitude / 2;

        base.Init(power);
        this.direction = dir.normalized;
        this.time = 0.0f;
        this.shoot_time = shoot_time;
        height = 0.0f;
    }

    private void FixedUpdate()
    {
        if (!active) return;

        if (time < shoot_time)
        {
            real_pos = start_pos + direction * (2 * m / shoot_time)*time;
            height = - max_height * (2/shoot_time * time - 1) * (2 / shoot_time * time - 1) + max_height;
            time += Time.deltaTime;
        }
        else
        {
            // splash!
            foreach(var collider in Physics2D.OverlapCircleAll(this.target_pos, this.radius)) {
                if(collider.gameObject.layer == 7)
                {
                    Damage(collider.gameObject);
                }
            }
            Pop();
            active = false;
        }

        transform.position = new Vector3(real_pos.x, real_pos.y + height, transform.position.z);
    }
}

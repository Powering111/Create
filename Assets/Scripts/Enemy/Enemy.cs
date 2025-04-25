using UnityEngine;

public abstract class Enemy : Mortal
{
    [SerializeField] EnemyBar health_bar;
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
            if(distance < recognition_range) {
                Recognize(player);
            }
        }
    }

    override protected void OnDamage()
    {
        health_bar.set_rate((float)health / (float)max_health);
    }


    protected abstract void Recognize(GameObject target);

}

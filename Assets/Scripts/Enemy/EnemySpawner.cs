using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject to_spawn;

    float ticks = 0;
    [SerializeField] float cooldown = 3.0f;
    void Update()
    {
        ticks += Time.deltaTime;
        while (ticks > cooldown)
        {
            Spawn();
            ticks -= cooldown;
        }
    }

    void Spawn()
    {
        Vector2 spawnPos = new Vector2(Random.Range(-10.0f, +10.0f), Random.Range(-10.0f, +10.0f));
        GameObject k = Instantiate(to_spawn, spawnPos, Quaternion.identity);
        k.GetComponent<Enemy>().Init(Player);
    }
}

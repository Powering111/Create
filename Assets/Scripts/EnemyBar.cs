using UnityEngine;

public class EnemyBar : MonoBehaviour, Bar
{
    [SerializeField]  float value;
    [SerializeField] Sprite[] sprites;
    SpriteRenderer spriteRenderer;

    float healthbar_visible_time = 0.0f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void set_rate(float value)
    {
        this.value = value;
        int idx = Mathf.RoundToInt((1.0f - value) * (sprites.Length - 1));
        spriteRenderer.sprite = sprites[idx];

        healthbar_visible_time = 3.0f;
    }

    private void Update()
    {
        if (healthbar_visible_time > 0.0f)
        {
            spriteRenderer.enabled = true;
            healthbar_visible_time -= Time.deltaTime;
        }
        else
        {
            spriteRenderer.enabled = false;
        }
    }
}

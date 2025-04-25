using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Sprite[] sprites;
    [SerializeField] float cooldown;
    [SerializeField] float curr_cooldown;

    public void SetCooldown(float cooldown, float curr_cooldown)
    {
        this.cooldown = cooldown;
        this.curr_cooldown = curr_cooldown;
        Propagate();
    }

    void Propagate()
    {
        float rate = 1.0f;
        if (cooldown > 0)
        {
            rate = 1.0f - curr_cooldown / cooldown;
        }

        int idx = Mathf.RoundToInt(rate * (sprites.Length - 1));
        image.sprite = sprites[idx];
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerSkill : MonoBehaviour
{
    [SerializeField] SkillSlot slot;
    [SerializeField] float cooldown;
    float curr_cooldown = 0;

    // set this when need to show hint
    protected bool showing_hint = false;

    void PropagateToSlot()
    {
        if (slot != null)
        {
            slot.SetCooldown(cooldown, curr_cooldown);
        }
    }
    private void Awake()
    {
        curr_cooldown = 0;
        PropagateToSlot();
    }
    protected bool Available()
    {
        return curr_cooldown <= 0;
    }

    protected void do_cooldown()
    {
        curr_cooldown = cooldown;
        PropagateToSlot();
    }

    void Update()
    {
        if(curr_cooldown > 0)
        {
            curr_cooldown -= Time.deltaTime;
            if(curr_cooldown < 0)
            {
                curr_cooldown = 0;
            }
            PropagateToSlot();
        }

        if (showing_hint && Available())
        {
            show_hint();
        }
        else
        {
            hide_hint();
        }
    }

    protected abstract void show_hint();
    protected abstract void hide_hint();

    protected abstract void use_skill(Vector2 pos);

    // called when input key is activated
    public abstract void action_start();

    // called when input key is released
    public abstract void action_end();
}

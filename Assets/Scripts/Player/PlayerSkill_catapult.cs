using Unity.VisualScripting;
using UnityEngine;

public class PlayerSkill_catapult: PlayerSkill
{
    [SerializeField] GameObject bubble_quadratic;
    [SerializeField] GameObject hint_w_splash, hint_w_reach;
    public override void action_start()
    {
        showing_hint = true;
    }

    Vector2 CalcPosition()
    {
        Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float mouse_distance = (mouse_pos - (Vector2)transform.position).magnitude;
        if (mouse_distance > 8.0f)
        {
            // skill reach is not long enough
            return (Vector2)(transform.position) + (mouse_pos - (Vector2)(transform.position)).normalized * 8.0f;
        }
        else
        {
            return mouse_pos;
        }
    }
    public override void action_end()
    {
        showing_hint = false;
        if (Available())
        {
            use_skill(CalcPosition());
        }
    }


    protected override void show_hint()
    {

        hint_w_reach.transform.position = transform.position;
        hint_w_reach.SetActive(true);
        hint_w_splash.transform.position = CalcPosition();
        hint_w_splash.SetActive(true);
    }

    protected override void hide_hint()
    {
        hint_w_reach.SetActive(false);
        hint_w_splash.SetActive(false);
    }

    protected override void use_skill(Vector2 pos)
    {
        GameObject new_bubble = Instantiate(bubble_quadratic, transform.position, Quaternion.identity);
        var target_position = CalcPosition();
        new_bubble.GetComponent<BubbleQuadratic>().Init(target_position, 50, 1.0f);

        do_cooldown();
    }
}

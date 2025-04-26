using UnityEngine;

public class PlayerSkill_smallbubble : PlayerSkill
{
    [SerializeField] GameObject bubble_linear;
    public override void action_start()
    {
        if (Available())
        {
            Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            use_skill(mouse_pos);
        }
    }
    public override void action_end()
    {
        
    }
    protected override void show_hint()
    {

    }
    protected override void hide_hint()
    {

    }
    protected override void use_skill(Vector2 pos)
    {
        // shoot small bubble
        GameObject new_bubble = Instantiate(bubble_linear, transform.position, Quaternion.identity);

        var attack_direction = pos - (Vector2)transform.position;
        new_bubble.GetComponent<BubbleLinear>().Init(attack_direction, 30, 1.0f, 5.0f);
        do_cooldown();
    }
}

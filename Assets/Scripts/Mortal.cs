using UnityEngine;
using UnityEngine.Events;

public abstract class Mortal : MonoBehaviour
{
    [SerializeField] protected int health = 100;
    [SerializeField] protected int max_health = 100;
    protected Animator animator;
    int anim_beinghit = Animator.StringToHash("beinghit");
    int anim_pop = Animator.StringToHash("pop");
    bool getting_damage = false;
    protected bool active = true;
    protected Rigidbody2D rb;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = max_health;
    }
    public void damage(int amount = 0)
    {
        if (active)
        {
            getting_damage = true;
            health -= amount;
            if (health <= 0)
            {
                health = 0;
                Pop();
            }
            OnDamage();
        }
    }
    abstract protected void OnDamage();

    private void Update()
    {
        if (getting_damage)
        {
            animator.SetBool(anim_beinghit, true);
            getting_damage = false;
        }
        else
        {
            animator.SetBool(anim_beinghit, false);
        }
    }

    protected void Pop()
    {
        active = false;
        // destroy on exit
        animator.SetBool(anim_beinghit, false);
        animator.SetBool(anim_pop, true);

        Collider2D component;
        if(TryGetComponent<Collider2D>(out component))
        {
            component.enabled = false;
        }
    }
}

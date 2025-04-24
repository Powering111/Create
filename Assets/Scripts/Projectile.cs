using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int power;
    Animator animator;
    int anim_pop = Animator.StringToHash("Pop");

    protected bool active = true, collidable = true;

    public void Init(int power = 10)
    {
        this.power = power;
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected void Pop()
    {
        // this animation will destroy gameObject on exit.
        animator.SetBool(anim_pop, true);
        active = false;
        collidable = false;
    }

    protected void Damage(GameObject target)
    {
        Mortal component;
        if(target.TryGetComponent<Mortal>(out component)){
            component.damage(power);
        }
    }
}

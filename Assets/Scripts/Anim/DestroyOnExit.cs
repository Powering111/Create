using UnityEngine;

// Put this class into the animation node
// Animation should not loop, transition to Exit node with exit time 1

public class DestroyOnExit : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.gameObject);
    }
}

using UnityEngine;

public class CheckoutItemAnimationState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("CheckoutItems", Mathf.Max(animator.GetInteger("CheckoutItems") - 1, 0));
    }
}

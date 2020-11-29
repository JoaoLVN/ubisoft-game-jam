using UnityEngine;

public class CheckoutItemAnimationState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log( Mathf.Max(animator.GetInteger("CheckoutItems") - 1, 0));
        animator.SetInteger("CheckoutItems", Mathf.Max(animator.GetInteger("CheckoutItems") - 1, 0));
    }
}

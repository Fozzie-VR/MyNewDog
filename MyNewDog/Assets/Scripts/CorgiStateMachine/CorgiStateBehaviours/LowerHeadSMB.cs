using UnityEngine;



public class LowerHeadSMB : StateMachineBehaviour
{
   public event System.Action LowerHeadEnteredEvent;
   
   public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
       base.OnStateEnter(animator, stateInfo, layerIndex);
       LowerHeadEnteredEvent?.Invoke();
   }
}

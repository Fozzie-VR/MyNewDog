using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBallSMB : StateMachineBehaviour
{
   public event System.Action PickupBallExitEvent;
   public event System.Action ChompBallEvent; 
   
   public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
       base.OnStateEnter(animator, stateInfo, layerIndex);
       ChompBallEvent?.Invoke();
   }
   
   public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
       base.OnStateExit(animator, stateInfo, layerIndex);
       PickupBallExitEvent?.Invoke();
   }
}

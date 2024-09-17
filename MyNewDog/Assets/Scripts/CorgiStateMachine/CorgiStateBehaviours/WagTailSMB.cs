using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagTailSMB : StateMachineBehaviour
{
    public event System.Action WagTailEnteredEvent;
    public event System.Action WagTailExitedEvent;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        WagTailEnteredEvent?.Invoke();
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        WagTailExitedEvent?.Invoke();
    }
    
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagTailSMB : StateMachineBehaviour
{
    public event System.Action WagTailExitedEvent;
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        WagTailExitedEvent?.Invoke();
    }
    
    
}

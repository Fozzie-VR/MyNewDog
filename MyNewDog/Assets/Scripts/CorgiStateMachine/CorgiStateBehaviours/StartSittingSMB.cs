using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSittingSMB : StateMachineBehaviour
{
    public event System.Action SitEnteredEvent;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        SitEnteredEvent?.Invoke();
    }
}

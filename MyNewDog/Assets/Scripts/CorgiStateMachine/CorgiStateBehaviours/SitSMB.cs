using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitSMB : StateMachineBehaviour
{
    public event Action SitEnteredEvent;
    public event Action SitExitedEvent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        SitEnteredEvent?.Invoke();
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        SitExitedEvent?.Invoke();
    }
}

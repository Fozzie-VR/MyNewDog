using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitSMB : StateMachineBehaviour
{
    public event Action SitExitedEvent;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        SitExitedEvent?.Invoke();
    }
}

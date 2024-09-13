using System;
using UnityEngine;

public class StandingSMB : StateMachineBehaviour
{
    public event Action StandingStateExitEvent;
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex )
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        StandingStateExitEvent?.Invoke();
    }
}

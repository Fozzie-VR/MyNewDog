using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StandState : IState
{
    public event Action<IState> StateExitedEvent;
    private Animator _animator;
    private int _isStandingHash;
    

    public StandState(Animator corgiAnimator)
    {
        _animator = corgiAnimator;
        //_animator.GetBehaviour<StandingSMB>().StandingStateExitEvent += OnStandingStateExit;
    }
    public void EnterState()
    {
        foreach (var parameter in _animator.parameters)              
        {
            if (parameter.type == AnimatorControllerParameterType.Bool)
            {
                _animator.SetBool(parameter.nameHash, false);
                
            }
        
            if (parameter.type == AnimatorControllerParameterType.Trigger)
            {
                _animator.ResetTrigger(parameter.nameHash);
            }
        }
    }

    public void ExitState()
    {
       Debug.Log("Exiting default state");
       StateExitedEvent?.Invoke(this);
    }
   
}

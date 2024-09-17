using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StandState : IState
{
    public event Action<IState> StateEnteredEvent;
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
        StateEnteredEvent?.Invoke(this);
    }

    public void ExitState()
    {
       Debug.Log("Exiting default state");
       StateExitedEvent?.Invoke(this);
    }
   
}

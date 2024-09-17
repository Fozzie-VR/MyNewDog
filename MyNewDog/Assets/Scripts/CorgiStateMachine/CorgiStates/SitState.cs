using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitState : IState
{
    public event Action<IState> StateEnteredEvent;
    public event Action<IState> StateExitedEvent;
    private CorgiBehaviour _corgiBehaviour;
    private Animator _animator;
    private int _sitBoolHash = Animator.StringToHash("isSitting");

    public SitState(Animator corgiAnimator)
    {
        _animator = corgiAnimator;
        var sitSMB = _animator.GetBehaviour<SitSMB>();
        sitSMB.SitExitedEvent += OnSitSMBExited;
        
        var startSittingSMB = _animator.GetBehaviour<StartSittingSMB>();
        startSittingSMB.SitEnteredEvent += OnSitAnimationEntered;

    }
    public void EnterState()
    {
       _animator.SetBool(_sitBoolHash, true);
    }

    private void OnSitAnimationEntered()
    {
        StateEnteredEvent?.Invoke(this);
    }

    public void ExitState()
    {
        _animator.SetBool(_sitBoolHash, false);
    }
    
    private void OnSitSMBExited()
    {
        ExitState();
        StateExitedEvent?.Invoke(this);
    }
}

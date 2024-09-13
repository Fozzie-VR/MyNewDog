using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitState : IState
{
    public event Action<IState> StateExitedEvent;
    private CorgiBehaviour _corgiBehaviour;
    private Animator _animator;
    private int _sitBoolHash = Animator.StringToHash("isSitting");

    public SitState(Animator corgiAnimator)
    {
        _animator = corgiAnimator;
        _animator.GetBehaviour<SitSMB>().SitExitedEvent += OnSitSMBExited;

    }
    public void EnterState()
    {
       _animator.SetBool(_sitBoolHash, true);
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

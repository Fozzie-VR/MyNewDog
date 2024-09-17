using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagTailState : IState
{
    public event Action<IState> StateEnteredEvent;
    public event Action<IState> StateExitedEvent;
    private Animator _animator;
    private int _isWagginTailHash = Animator.StringToHash("isWaggingTail");
    public WagTailState(Animator corgiAnimator)
    {
        _animator = corgiAnimator;
        var wagTailSMB = _animator.GetBehaviour<WagTailSMB>();
        wagTailSMB.WagTailEnteredEvent += OnWagTailEntered;
        wagTailSMB.WagTailExitedEvent += OnWagTailExited;
    }

    public void EnterState()
    {
        _animator.SetBool(_isWagginTailHash, true);
    }
    
    private void OnWagTailEntered()
    {
        StateEnteredEvent?.Invoke(this);
    }

    public void ExitState()
    {
        _animator.SetBool(_isWagginTailHash, false);
    }
    
    private void OnWagTailExited()
    {
        StateExitedEvent?.Invoke(this);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ReturnToPlayerState : IState
{
    public event Action<IState> StateExitedEvent;
    public event Action ReturnedToPlayerEvent;
    public event Action InRangeEvent;
    private Animator _animator;
    private int _isReturningHash = Animator.StringToHash("isReturning");

    public ReturnToPlayerState(Animator corgiAnimator)
    {
        _animator = corgiAnimator;
        _animator.GetBehaviour<ReturnToPlayerSMB>().ReachedPlayerEvent += OnReachedPlayer;
        _animator.GetBehaviour<ReturnToPlayerSMB>().ReturnToPlayerExitedEvent += OnReturnToPlayerSMBExited;
    }
    public void EnterState()
    {
       _animator.SetBool(_isReturningHash, true);
    }

    public void ExitState()
    {
        _animator.SetBool(_isReturningHash, false);
    }
    
    private void OnReachedPlayer()
    {
        //if has ball, then drop it
        Transform rootTransform = _animator.transform.root;
        XRSocketInteractor xrSocketInteractor = rootTransform.GetComponentInChildren<XRSocketInteractor>();
        if (xrSocketInteractor && xrSocketInteractor.isSelectActive)
        {
            xrSocketInteractor.enabled = false;
        }
        ReturnedToPlayerEvent?.Invoke();
        
       
    }
    
    private void OnReturnToPlayerSMBExited()
    {
        StateExitedEvent?.Invoke(this);
    }
}

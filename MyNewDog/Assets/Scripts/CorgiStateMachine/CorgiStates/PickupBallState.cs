using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class PickupBallState : IState
{
    public event Action<IState> StateExitedEvent;
    public event Action<Transform> BallChompedEvent;
    CorgiBehaviour _corgiBehavior;
    private Animator _animator;
    private int _pickupBallTriggerHash;
    private XRSocketInteractor _socketInteractor;
    
    private Transform _targetBall;

    public PickupBallState(Animator animator)
    {
        _animator = animator;
        _pickupBallTriggerHash = Animator.StringToHash("pickupBall");
        _animator.GetBehaviour<PickupBallSMB>().PickupBallExitEvent += OnPickupAnimationFinished;
        Transform root = _animator.transform.root;
        _socketInteractor = root.GetComponentInChildren<XRSocketInteractor>();
    }

    public void SetTargetBall(Transform ball)
    {
        _targetBall = ball;
    }
       
    public void EnterState()
    {
        _animator.SetTrigger(_pickupBallTriggerHash);
        _socketInteractor.enabled = true;
    }

    public void ExitState()
    {
        StateExitedEvent?.Invoke(this);
    }

    public void OnPickupAnimationFinished()
    {
        //make sure mouth socket is selecting ball
        var selectedInteractable = _socketInteractor.GetOldestInteractableSelected();
        if (selectedInteractable != null && selectedInteractable.transform == _targetBall)
        {
            BallChompedEvent?.Invoke(_targetBall);
            //ExitState();
        }
        else
        {
            XRInteractionManager interactionManager = _socketInteractor.interactionManager;
            IXRSelectInteractor selectInteractor = _socketInteractor;
            IXRSelectInteractable selectInteractable = _targetBall.GetComponent<IXRSelectInteractable>();
            interactionManager.SelectEnter(selectInteractor, selectInteractable);
            BallChompedEvent?.Invoke(_targetBall);
            
        }
    }
}

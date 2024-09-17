using System;
using UnityEngine;

public class ChaseBallState : IState
{
    public event Action<IState> StateEnteredEvent;
    public event Action<IState> StateExitedEvent;
    public event Action<Transform> BallInRangeEvent;
    
    public event Action BallLostEvent;
    
    private CorgiBehaviour _corgiBehavior;
    private Animator _animator;
    private int _isChasing;

    private ChaseBallSMB _chaseBallSMB;

    public ChaseBallState(Animator corgiAnimator)
    {
        _animator = corgiAnimator;
        _isChasing = Animator.StringToHash("isChasing");
        _chaseBallSMB = _animator.GetBehaviour<ChaseBallSMB>();
        _chaseBallSMB.ChaseBallEnteredEvent += OnChaseAnimationEntered;
        _chaseBallSMB.SMBExitedEvent += OnSMBExited;
        _chaseBallSMB.InPickupRangeEvent += OnBallInPickupRange;
        _chaseBallSMB.BallLostEvent += OnBallLost;

    }

   
    public void EnterState()
    {
        _animator.SetBool(_isChasing, true);
    }

    private void OnChaseAnimationEntered()
    {
        StateEnteredEvent?.Invoke(this);
    }
    
    private void OnBallLost()
    {
        BallLostEvent?.Invoke();
    }

    public void SetTargetBall(Transform ball)
    {
        _chaseBallSMB.SetTargetBall(ball);
    }

    private void OnBallInPickupRange(Transform ball)
    {
        BallInRangeEvent?.Invoke(ball);
    }

    public void ExitState()
    {
        _animator.SetBool(_isChasing, false);
       
    }

    private void OnSMBExited()
    {
        StateExitedEvent?.Invoke(this);
    }
}

using System;
using UnityEngine;
using UnityEngine.Animations;


public class ChaseBallSMB : StateMachineBehaviour
{

    private Transform _targetBall;
    private Transform _corgiTransform;
    public event Action SMBExitedEvent;
    public event Action<Transform> InPickupRangeEvent;
    public event Action BallLostEvent;

   
    public void SetTargetBall(Transform ball)
    {
        _targetBall = ball;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        _corgiTransform = animator.transform;
        
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (_targetBall is null)
        {
            Debug.Log("target ball is null");
            return;
        }
        ConfirmBallIsReachable();
        FaceBall();
        MoveTowardsBall();
        
    }

    private void ConfirmBallIsReachable()
    {
        if (_corgiTransform.transform.position.y - _targetBall.position.y > 5f)
        {
            BallLostEvent?.Invoke();
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        _targetBall = null;
        SMBExitedEvent?.Invoke();
    }

    private void FaceBall()
    {
        Vector3 direction = _targetBall.position - _corgiTransform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        _corgiTransform.rotation = Quaternion.Lerp(_corgiTransform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void MoveTowardsBall()
    {
        Vector3 direction = _corgiTransform.forward;
        direction.y = 0f;
        float distance = Vector3.Distance(_corgiTransform.position, _targetBall.position);
        if (distance > 0.3f)
        {
            _corgiTransform.Translate(direction * Time.deltaTime, Space.World);
        }
        else
        {
            //in pickup range event
            InPickupRangeEvent?.Invoke(_targetBall);
        }
    }
    
}

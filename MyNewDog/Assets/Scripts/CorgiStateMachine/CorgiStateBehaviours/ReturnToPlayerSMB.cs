using UnityEngine;

public class ReturnToPlayerSMB : StateMachineBehaviour
{
    public event System.Action ReturnToPlayerExitedEvent;
    public event System.Action ReachedPlayerEvent;
    Transform _playerTransform;
    Transform _corgiTransform;
    
    float _reachedPlayerDistance = 1.5f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _corgiTransform = animator.transform;
        _playerTransform = Camera.main.transform;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        FacePlayer();
        MoveTowardsPlayer();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        
        ReturnToPlayerExitedEvent?.Invoke();
    }

    private void FacePlayer()
    {
        Vector3 direction = _playerTransform.position - _corgiTransform.position;
        direction.y = 0f;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        _corgiTransform.rotation = Quaternion.Lerp(_corgiTransform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = _corgiTransform.forward;
        direction.y = 0f;
        float distance = Vector3.Distance(_corgiTransform.position, _playerTransform.position);
        if (distance > _reachedPlayerDistance)
        {
            _corgiTransform.Translate(direction * Time.deltaTime, Space.World);
        }
        else
        {
           ReachedPlayerEvent?.Invoke();
        }
    }
}

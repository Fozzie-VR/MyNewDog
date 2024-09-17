using UnityEngine;


public class CorgiBehaviour : MonoBehaviour
{
    private Animator _animator;

    private IState _currentState;
    private IState _nextState;
   
    private ChaseBallState _corgiChaseBallState;
    private PickupBallState _pickupBallState;
    private SitState _sitState;
    private StandState _standState;
    private WagTailState _wagTailState;
    private ReturnToPlayerState _returnToPlayerState;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        InitializeStates();
        _currentState = _standState;
    }

    private void Start()
    {
        BallRegisterer ballRegisterer = GetComponent<BallRegisterer>();
        if (ballRegisterer)
        {
            ballRegisterer.BallThrownEvent += OnBallThrown;
            ballRegisterer.BallPickedUpEvent += OnBallPickedUp;
        }
        
        TailWagger tailWagger = GetComponent<TailWagger>();
        if (tailWagger)
        {
            tailWagger.PettingEvent += OnPetting;
            tailWagger.PettingStoppedEvent += OnPettingStopped;
        }
        
    }

    private void InitializeStates()
    {
        _standState = new StandState(_animator);
        _standState.StateEnteredEvent += OnStateEntered;
        _standState.StateExitedEvent += OnCurrentStateExited;
        
        _wagTailState = new WagTailState(_animator);
        _wagTailState.StateEnteredEvent += OnStateEntered;
        _wagTailState.StateExitedEvent += OnCurrentStateExited;
        
        _returnToPlayerState = new ReturnToPlayerState(_animator);
        _returnToPlayerState.StateEnteredEvent += OnStateEntered;
        _returnToPlayerState.StateExitedEvent += OnCurrentStateExited;
        _returnToPlayerState.ReturnedToPlayerEvent += OnReturnedToPlayer;
          
        _sitState = new SitState(_animator);
        _sitState.StateExitedEvent += OnCurrentStateExited;
        _sitState.StateEnteredEvent += OnStateEntered;
        
        _corgiChaseBallState = new ChaseBallState(_animator);
        _corgiChaseBallState.StateEnteredEvent += OnStateEntered;
        _corgiChaseBallState.StateExitedEvent += OnCurrentStateExited;
        _corgiChaseBallState.BallInRangeEvent += PickupBall;
        _corgiChaseBallState.BallLostEvent += OnBallLost;
        
        _pickupBallState = new PickupBallState(_animator);
        _pickupBallState.StateEnteredEvent += OnStateEntered;
        _pickupBallState.BallChompedEvent += OnBallChomped;
        _pickupBallState.StateExitedEvent += OnCurrentStateExited;
        
    }

   


    private void OnBallThrown(Transform ball)
    {
        if (_currentState == _standState || _currentState == _wagTailState || _currentState == _sitState)
        {
            ChaseBall(ball);
        }
    }

    private void OnBallPickedUp()
    {
        if (_currentState == _standState || _currentState == _wagTailState)
        {
            Sit();
        }
    }
    
    private void OnBallChomped(Transform ball)
    {
        if (_currentState == _pickupBallState)
        {
            ReturnToPlayer();
        }
    }

    private void OnBallLost()
    {
        ReturnToPlayer();
    }
    
    private void OnReturnedToPlayer()
    {
       Stand();
    }
    
    private void OnPetting()
    {
        if (_currentState != _wagTailState)
        {
            WagTail();
        }
    }
    
    private void OnPettingStopped()
    {
        if (_currentState == _wagTailState)
        {
            Stand();
        }
    }

    private void OnStateEntered(IState state)
    {
        _currentState = state;
    }

    private void OnCurrentStateExited(IState state)
    {
        if (state != _currentState)
        {
            Debug.LogError("Trying to exit a state that is not the current state");
        }
        
        EnterNextState();
    }

    private void EnterNextState()
    {
        _nextState.EnterState();
    }

    
    #region CorgiBehaviours
    
    //when waiting for ball throw
    private void Sit()
    {
        _nextState = _sitState;
        _currentState.ExitState();
    }
    
    //default state
    private void Stand()
    {
        _nextState = _standState;
        _currentState.ExitState();
    }
    
    
    //when player is petting dog
    private void WagTail()
    {
        _nextState = _wagTailState;
        _currentState.ExitState();
    }

    private void ChaseBall(Transform ball)
    {
        _corgiChaseBallState.SetTargetBall(ball);
        _nextState = _corgiChaseBallState;
        _currentState.ExitState();
    }
    
    private void PickupBall(Transform ball)
    {
        _pickupBallState.SetTargetBall(ball);
        _nextState = _pickupBallState;
        _currentState.ExitState();
    }
    
    private void ReturnToPlayer()
    {
        _nextState = _returnToPlayerState;
        _currentState.ExitState();
    }

   #endregion





}

using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class BallRegisterer : MonoBehaviour
{
    [SerializeField] ObjectSpawner _ballSpawner;
    [SerializeField] private GameObject[] _ballsInScene;
    
    public event Action<Transform> BallThrownEvent;
    public event Action BallPickedUpEvent;

    private void Start()
    {
        if (_ballSpawner)
        {
            _ballSpawner.objectSpawned += OnBallSpawned;
        }

        if (_ballsInScene != null && _ballsInScene.Length > 0)
        {
            foreach (var ball in _ballsInScene)
            {
                XRGrabInteractable grabInteractable = ball.GetComponent<XRGrabInteractable>();
                grabInteractable.selectEntered.AddListener(OnBallPickedUp);
                grabInteractable.selectExited.AddListener(OnBallReleased);
            }
        }
    }

    private void OnBallSpawned(GameObject ball)
    {
        XRGrabInteractable grabInteractable = ball.GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnBallPickedUp);
        grabInteractable.selectExited.AddListener(OnBallReleased);
    }

    private void OnBallPickedUp(SelectEnterEventArgs arg0)
    {
        //check to make sure it was the player picking up the ball
        IXRSelectInteractor interactor = arg0.interactorObject;
        if (interactor is NearFarInteractor)
        {
            BallPickedUpEvent?.Invoke();
        }
    }
    
    private void OnBallReleased(SelectExitEventArgs arg0)
    {
        //check to make sure it was the player dropping the ball
        IXRSelectInteractor interactor = arg0.interactorObject;
        if (interactor is NearFarInteractor)
        {
            Transform ball = arg0.interactableObject.transform;
            BallThrownEvent?.Invoke(ball);
        }
    }
}

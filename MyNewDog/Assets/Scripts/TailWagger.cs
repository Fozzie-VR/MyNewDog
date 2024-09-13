using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class TailWagger : MonoBehaviour
{
    // Start is called before the first frame update
    public event System.Action PettingEvent;
    public event System.Action PettingStoppedEvent;
    void Start()
    {
        var xrInteractable = GetComponent<XRBaseInteractable>();
        xrInteractable.hoverEntered.AddListener(OnHover);
        xrInteractable.hoverExited.AddListener(OnHoverExit);
        
    }

    private void OnHoverExit(HoverExitEventArgs arg0)
    {
        if (arg0.interactorObject is XRSocketInteractor)
        {
            return;
        }
        PettingStoppedEvent?.Invoke();
    }

    private void OnHover(HoverEnterEventArgs arg0)
    {
        Debug.Log(" Tail wagging " + arg0.interactorObject.transform.name);
        if (arg0.interactorObject is XRSocketInteractor)
        {
            return;
        }
        PettingEvent?.Invoke();
    }

   
}

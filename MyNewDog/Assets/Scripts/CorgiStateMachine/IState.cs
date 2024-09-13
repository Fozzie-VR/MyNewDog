using System;
using UnityEngine;

public interface IState
{
    event Action<IState> StateExitedEvent;

    void EnterState();

    void ExitState();

}

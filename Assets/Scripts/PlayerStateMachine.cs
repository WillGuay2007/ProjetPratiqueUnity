using System.Diagnostics;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerController controller { get; }
    public PlayerState currentState { get; private set; }

    public PlayerStateMachine(PlayerController plrcontroller)
    {
        controller = plrcontroller;
    }

    public void Initialize(PlayerState defaultState)
    {
        currentState = defaultState;
        currentState.Enter();
    }

    public void SetState(PlayerState newState)
    {
        if (newState == currentState)
            return;

        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
}

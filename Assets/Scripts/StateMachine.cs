using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine<Controller>
{
    public Controller controller;
    public IState currentState;
    public Dictionary<string, IState> states;
    
    public StateMachine(Controller controller)
    {
        this.controller = controller;
    }

    public abstract void Update();
    public void SetState(IState newState)
    {
        if (newState != null && newState != currentState)
        {
            currentState.Exit();
            currentState = newState;
            newState.Enter();
        }
    }

}

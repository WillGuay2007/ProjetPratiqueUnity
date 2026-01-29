public class StateMachine<TController>
{
    public TController Controller { get; }
    public State<TController> CurrentState { get; private set; }

    public StateMachine(TController controller)
    {
        Controller = controller;
    }

    public void Update()
    {
        CurrentState?.Update();
    }

    public void SetState(State<TController> newState)
    {
        if (newState == CurrentState)
            return;

        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}

public abstract class State<TController>
{
    protected TController Controller;
    protected StateMachine<TController> StateMachine;

    protected State(StateMachine<TController> stateMachine)
    {
        StateMachine = stateMachine;
        Controller = stateMachine.Controller;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}

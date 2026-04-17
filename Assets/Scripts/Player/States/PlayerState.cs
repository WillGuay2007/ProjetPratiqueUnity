using UnityEngine;

public abstract class PlayerState
{
    protected PlayerController player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;
    protected float startTime;
    protected bool transitioned; //Pour que quand une classe parent change de state, les enfants run pas. Exemple: If transitioned return
    private string animBoolName;

    protected PlayerState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool)
    {
        stateMachine = plrStateMachine;
        player = playerController;
        playerData = plrData;
        animBoolName = animBool;
    }

    public virtual void Enter() {
        transitioned = false;
        //Debug.Log("Entering " + GetType().Name);
        DoChecks();
        player.Animator.SetBool(animBoolName, true);
        startTime = Time.time;
    }
    public virtual void Exit() {
        player.Animator.SetBool(animBoolName, false);
    }
    public virtual void LogicUpdate() { }
    public virtual void PhysicsUpdate() {
        DoChecks();
    }
    public virtual void DoChecks(){ }
    public virtual bool CanTransitionTo(PlayerState newState) => true;


    protected void Transition(PlayerState newState)
    {
        transitioned = true;
        stateMachine.SetState(newState);
    }

}

using UnityEngine;

public abstract class PlayerState
{
    protected PlayerController controller;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;
    protected float startTime;
    private string animBoolName;

    protected PlayerState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool)
    {
        stateMachine = plrStateMachine;
        controller = playerController;
        playerData = plrData;
        animBoolName = animBool;
    }

    public virtual void Enter() {
        DoChecks();
        Debug.Log(animBoolName);
        controller.animator.SetBool(animBoolName, true);
        startTime = Time.time;
    }
    public virtual void Exit() {
        controller.animator.SetBool(animBoolName, false);
    }
    public virtual void LogicUpdate() { }
    public virtual void PhysicsUpdate() {
        DoChecks();
    }
    public virtual void DoChecks(){ }
}

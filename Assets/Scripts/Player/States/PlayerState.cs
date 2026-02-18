using UnityEngine;

public abstract class PlayerState
{
    protected PlayerController player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;
    protected float startTime;
    private string animBoolName;

    protected PlayerState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool)
    {
        stateMachine = plrStateMachine;
        player = playerController;
        playerData = plrData;
        animBoolName = animBool;
    }

    public virtual void Enter() {
        //Debug.Log("Entering " + GetType().Name);
        DoChecks();
        player.animator.SetBool(animBoolName, true);
        startTime = Time.time;
    }
    public virtual void Exit() {
        player.animator.SetBool(animBoolName, false);
    }
    public virtual void LogicUpdate() { }
    public virtual void PhysicsUpdate() {
        DoChecks();
    }
    public virtual void DoChecks(){ }
}

using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    protected int yInput;

    public PlayerGroundedState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool) : base(playerController, plrStateMachine, plrData, animBool)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.playerInputManager.NormInputX;
        yInput = player.playerInputManager.NormInputY;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

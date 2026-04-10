using UnityEngine;

public class PlayerAbilityState : PlayerState
{

    protected bool isAbilityDone;
    protected int xInput;
    protected int yInput;
    protected bool isGrounded;

    public PlayerAbilityState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool) : base(playerController, plrStateMachine, plrData, animBool)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfOnGround();
    }

    public override void Enter()
    {
        base.Enter();
        isAbilityDone = false;

        xInput = player.input.NormInputX;
        yInput = player.input.NormInputY;

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.input.NormInputX;
        yInput = player.input.NormInputY;

        if (isAbilityDone)
        {
            Transition(player.inAirState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    protected int yInput;
    private bool m_jumpInput;
    private bool m_isGrounded;

    public PlayerGroundedState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool) : base(playerController, plrStateMachine, plrData, animBool)
    {
    }

    protected bool HandleGroundedTransitions()
    {
        xInput = player.playerInputManager.NormInputX;
        m_jumpInput = player.playerInputManager.JumpInput;

        if (m_jumpInput && player.jumpState.CanJump())
        {
            player.playerInputManager.UseJumpInput();
            stateMachine.SetState(player.jumpState);
            return true;
        }
        else if (!m_isGrounded)
        {
            player.inAirState.StartCoyoteTime();
            stateMachine.SetState(player.inAirState);
            return true;
        }
        return false;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        m_isGrounded = player.CheckIfOnGround();
    }

    public override void Enter()
    {
        base.Enter();

        player.jumpState.ResetAmoutOfJumpsLeft();
        player.dashState.ResetAmountOfAirDashesLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    protected int yInput;
    private bool m_jumpInput;
    private bool m_dashInput;
    private bool m_isGrounded;
    private bool m_swapGravityInput;

    public PlayerGroundedState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool) : base(playerController, plrStateMachine, plrData, animBool)
    {
    }

    protected bool HandleGroundedTransitions()
    {
        if (m_swapGravityInput)
        {
            player.input.UseSwapGravityInput();
            player.gravity.SwapPlayerGravity();
        }

        if (m_jumpInput && player.jumpState.CanJump())
        {
            player.input.UseJumpInput();
            stateMachine.SetState(player.jumpState);
            return true;
        }
        else if (m_dashInput && player.dashState.CanDash() && (xInput != 0 || yInput != 0))
        {
            player.input.UseDashInput();
            stateMachine.SetState(player.dashState);
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
        player.dashState.ResetAmountOfDashesLeft();
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
        m_jumpInput = player.input.JumpInput;
        m_dashInput = player.input.DashInput;
        m_swapGravityInput = player.input.SwapGravityInput;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

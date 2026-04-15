using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    protected int yInput;
    private bool m_jumpInput;
    private bool m_dashInput;
    private bool m_grabInput;
    private bool m_isGrounded;
    private bool m_swapGravityInput;
    private bool m_isTouchingWall;
    public PlayerGroundedState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool) : base(playerController, plrStateMachine, plrData, animBool)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        m_isGrounded = player.CheckIfOnGround();
        m_isTouchingWall = player.CheckIfFacingWall();
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
        xInput = player.InputController.NormInputX;
        yInput = player.InputController.NormInputY;
        m_grabInput = player.InputController.GrabInput;
        m_jumpInput = player.InputController.JumpInput;
        m_dashInput = player.InputController.DashInput;
        m_swapGravityInput = player.InputController.SwapGravityInput;

        if (m_swapGravityInput)
        {
            player.InputController.UseSwapGravityInput();
            player.GravityController.SwapPlayerGravity();
        }

        if (m_isTouchingWall && m_grabInput)
        {
            Transition(player.grabState);
        }
        else if (m_isTouchingWall && !m_isGrounded) Transition(player.wallSlideState);
        else if (m_jumpInput && player.jumpState.CanJump())
        {
            player.InputController.UseJumpInput();
            Transition(player.jumpState);
        }
        else if (m_dashInput && player.dashState.CanDash() && (xInput != 0 || yInput != 0))
        {
            player.InputController.UseDashInput();
            Transition(player.dashState);
        }
        else if (!m_isGrounded)
        {
            player.inAirState.StartCoyoteTime();
            Transition(player.inAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

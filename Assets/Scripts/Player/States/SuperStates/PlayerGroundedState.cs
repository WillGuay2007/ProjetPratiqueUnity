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
        m_isGrounded = player.SurfaceDetector.IsOnGround();
        m_isTouchingWall = player.SurfaceDetector.IsFacingWall();
    }

    public override void Enter()
    {
        base.Enter();

        stateMachine.jumpState.ResetAmoutOfJumpsLeft();
        stateMachine.dashState.ResetAmountOfDashesLeft();
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
            Transition(stateMachine.grabState);
        }
        else if (m_isTouchingWall && !m_isGrounded) Transition(stateMachine.wallSlideState);
        else if (m_jumpInput && stateMachine.jumpState.CanJump())
        {
            player.InputController.UseJumpInput();
            Transition(stateMachine.jumpState);
        }
        else if (m_dashInput && stateMachine.dashState.CanDash() && (xInput != 0 || yInput != 0))
        {
            player.InputController.UseDashInput();
            Transition(stateMachine.dashState);
        }
        else if (!m_isGrounded)
        {
            stateMachine.inAirState.StartCoyoteTime();
            Transition(stateMachine.inAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

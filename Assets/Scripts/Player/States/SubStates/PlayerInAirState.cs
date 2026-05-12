using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private int xInput;
    private int yInput;
    private bool m_isGrounded;
    private bool m_jumpInput;
    private bool m_dashInput;
    private bool m_coyoteTime;
    private bool m_jumpInputStop;
    private bool m_isJumping;
    private bool m_isFacingWall;
    private bool m_grabInput;
    private bool m_isTouchingWall;
    public PlayerInAirState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool) : base(playerController, plrStateMachine, plrData, animBool)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        m_isGrounded = player.SurfaceDetector.IsOnGround();
        m_isFacingWall = player.SurfaceDetector.IsFacingWall();
        m_isTouchingWall = player.SurfaceDetector.IsTouchingWall();
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.PlayerInfosWindow.SetCanFlipText("Can switch gravity: False");
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
        m_jumpInput = player.InputController.JumpInput;
        m_jumpInputStop = player.InputController.JumpInputStop;
        m_dashInput = player.InputController.DashInput;
        m_grabInput = player.InputController.GrabInput;

        CheckCoyoteTime();

        CheckJumpMultiplier();

        if (m_isFacingWall && m_grabInput)
        {
            Transition(stateMachine.grabState);
        }
        else if (m_isFacingWall && player.MovementController.IsFalling())
        {
            Transition(stateMachine.wallSlideState);
        }
        else if (m_isTouchingWall && m_jumpInput)
        {
            player.InputController.UseJumpInput();
            int faceWallMultiplier = m_isFacingWall ? 1 : -1;
            player.MovementController.SetVelocityX(playerData.wallBounceXVelocity * -player.DirectionController.facingXDirection * faceWallMultiplier);
            Transition(stateMachine.jumpState);
        }
        else if (m_isGrounded && Mathf.Abs(player.RigidBody.linearVelocity.y) < 0.01f) //Abs parce que je dois considerer le gravity swap.
        {
            Transition(stateMachine.moveState);
        }
        else if (m_jumpInput && stateMachine.jumpState.CanJump()) {
            player.InputController.UseJumpInput();
            Transition(stateMachine.jumpState);
        }
        else if (m_dashInput && stateMachine.dashState.CanDash() && (xInput != 0 || yInput != 0))
        {
            player.InputController.UseDashInput();
            Transition(stateMachine.dashState);
        }
        else
        {
            player.DirectionController.ShouldFlipX(xInput);
            player.MovementController.AccelerateXVelocity(xInput * playerData.movementVelocity, playerData.airFrictionX);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckCoyoteTime()
    {
        if (m_coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            m_coyoteTime = false;
            stateMachine.jumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    private void CheckJumpMultiplier()
    {
        if (m_isJumping)
        {
            if (m_jumpInputStop)
            {
                player.MovementController.SetVelocityY(player.MovementController.velocity.y * playerData.jumpHoldMultiplier);
                m_isJumping = false;
            }
            else if (player.MovementController.IsFalling())
            {
                m_isJumping = false;
            }
        }
    }

    public void StartCoyoteTime() => m_coyoteTime = true;
    public void SetIsJumping() => m_isJumping = true;
}

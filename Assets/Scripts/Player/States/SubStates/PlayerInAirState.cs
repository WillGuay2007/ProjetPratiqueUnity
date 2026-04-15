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
        m_isGrounded = player.CheckIfOnGround();
        m_isFacingWall = player.CheckIfFacingWall();
        m_isTouchingWall = player.CheckIfTouchingWall();
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
            Transition(player.grabState);
        }
        else if (m_isFacingWall && player.IsFalling())
        {
            Transition(player.wallSlideState);
        }
        else if (m_isTouchingWall && m_jumpInput)
        {
            player.InputController.UseJumpInput();
            int faceWallMultiplier = m_isFacingWall ? 1 : -1;
            player.MovementController.SetVelocityX(playerData.wallBounceXVelocity * -player.facingXDirection * faceWallMultiplier);
            Transition(player.jumpState);
        }
        else if (m_isGrounded && Mathf.Abs(player.rigidBody.linearVelocity.y) < 0.01f) //Abs parce que je dois considerer le gravity swap.
        {
            Transition(player.moveState);
        }
        else if (m_jumpInput && player.jumpState.CanJump()) {
            player.InputController.UseJumpInput();
            Transition(player.jumpState);
        }
        else if (m_dashInput && player.dashState.CanDash() && (xInput != 0 || yInput != 0))
        {
            player.InputController.UseDashInput();
            Transition(player.dashState);
        }
        else
        {
            player.CheckIfShouldFlipX(xInput);
            player.MovementController.AdjustVelocityX_Constant(xInput * playerData.movementVelocity, playerData.airFrictionX);
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
            player.jumpState.DecreaseAmountOfJumpsLeft();
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
            else if (player.IsFalling())
            {
                m_isJumping = false;
            }
        }
    }

    public void StartCoyoteTime() => m_coyoteTime = true;
    public void SetIsJumping() => m_isJumping = true;
}

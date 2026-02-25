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
    private bool m_isTouchingWall;
    private bool m_grabInput;
    public PlayerInAirState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool) : base(playerController, plrStateMachine, plrData, animBool)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        m_isGrounded = player.CheckIfOnGround();
        m_isTouchingWall = player.CheckIfIsTouchingWall();
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

        xInput = player.input.NormInputX;
        yInput = player.input.NormInputY;
        m_jumpInput = player.input.JumpInput;
        m_jumpInputStop = player.input.JumpInputStop;
        m_dashInput = player.input.DashInput;
        m_grabInput = player.input.GrabInput;

        if (m_isTouchingWall)
        {
            if (m_grabInput) player.stateMachine.SetState(player.grabState);
            else if (player.IsFalling()) player.stateMachine.SetState(player.wallSlideState);
        }

        CheckCoyoteTime();

        CheckJumpMultiplier();

        if (m_isGrounded && Mathf.Abs(player.rigidBody.linearVelocity.y) < 0.01f) //Abs parce que je dois considerer le gravity swap.
        {
            stateMachine.SetState(player.moveState);
        }
        else if (m_jumpInput && player.jumpState.CanJump()) {
            player.input.UseJumpInput();
            stateMachine.SetState(player.jumpState);
        }
        else if (m_dashInput && player.dashState.CanDash() && (xInput != 0 || yInput != 0))
        {
            player.input.UseDashInput();
            stateMachine.SetState(player.dashState);
        }
        else
        {
            player.CheckIfShouldFlipX(xInput);
            player.movement.AdjustVelocityX_Constant(xInput * playerData.movementVelocity, playerData.airFrictionX);
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
                player.movement.SetVelocityY(player.movement.velocity.y * playerData.jumpHoldMultiplier);
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

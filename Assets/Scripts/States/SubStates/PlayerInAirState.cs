using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private int xInput;
    private bool m_isGrounded;
    private bool m_jumpInput;
    private bool m_coyoteTime;
    private bool m_jumpInputStop;
    private bool m_isJumping;
    public PlayerInAirState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool) : base(playerController, plrStateMachine, plrData, animBool)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        m_isGrounded = player.CheckIfOnGround();
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

        CheckCoyoteTime();

        xInput = player.playerInputManager.NormInputX;
        m_jumpInput = player.playerInputManager.JumpInput;
        m_jumpInputStop = player.playerInputManager.JumpInputStop;

        CheckJumpMultiplier();

        if (m_isGrounded && player.rigidBody.linearVelocity.y < 0.01f)
        {
            stateMachine.SetState(player.idleState);
        }
        else if (m_jumpInput && player.jumpState.CanJump()) {
            player.playerInputManager.UseJumpInput();
            stateMachine.SetState(player.jumpState);
        }
        else
        {
            player.CheckIfShouldFlip(xInput);
            player.SetVelocityX(playerData.movementVelocity * xInput);
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
                player.SetVelocityY(player.currentVelocity.y * playerData.jumpHoldMultiplier);
                m_isJumping = false;
            }
            else if (player.currentVelocity.y <= 0f)
            {
                m_isJumping = false;
            }
        }
    }

    public void StartCoyoteTime() => m_coyoteTime = true;
    public void SetIsJumping() => m_isJumping = true;
}

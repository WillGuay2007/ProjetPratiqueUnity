using UnityEngine;

public class PlayerWallSlideState : PlayerTouchWallState
{
    private bool m_isGrounded;
    public PlayerWallSlideState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool) : base(playerController, plrStateMachine, plrData, animBool)
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
        player.gravity.DisablePlayerGravity();
        player.movement.SetVelocityX(0);
    }

    public override void Exit()
    {
        base.Exit();
        player.gravity.SetDefaultGravityConsideringGravSwap();
        player.movement.SetVelocityX(playerData.wallSlideExitXVelocity * player.facingXDirection);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isStillOnWall) return;
        if (grabInput)
        {
            player.stateMachine.SetState(player.grabState);
            return;
        }
        if (m_isGrounded)
        {
            player.stateMachine.SetState(player.idleState);
            return;
        }
        if (xInput != player.facingXDirection && xInput != 0f)
        {
            player.CheckIfShouldFlipX(xInput);
            player.stateMachine.SetState(player.inAirState);
            return;
        }
        player.movement.SetVelocityX(0);
        player.movement.AdjustVelocityY_Constant(-Mathf.Abs(playerData.maxWallSlideVelocity) * player.facingYDirection, playerData.wallSlideFriction);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

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
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (transitioned)
        {
            return;
        }
        if (grabInput)
        {
            Transition(player.grabState);
            return;
        }
        else if (m_isGrounded)
        {
            Transition(player.idleState);
            return;
        }
        else if (jumpInput)
        {
            Transition(player.jumpState);
            player.movement.SetVelocityX(playerData.wallBounceXVelocity * -player.facingXDirection);
            return;
        }
        else if (xInput != player.facingXDirection && xInput != 0f)
        {
            player.CheckIfShouldFlipX(xInput);
            Transition(player.inAirState);
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

using UnityEngine;

public class PlayerClimbState : PlayerTouchWallState
{
    public PlayerClimbState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool) : base(playerController, plrStateMachine, plrData, animBool)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.gravity.DisablePlayerGravity();
        player.movement.ResetVelocity();
    }

    public override void Exit()
    {
        base.Exit();
        player.gravity.SetDefaultGravityConsideringGravSwap();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isStillOnWall) return;
        player.movement.SetVelocityX(0);
        player.movement.SetVelocityY(yInput * playerData.wallClimbSpeed);
        if (!grabInput)
        {
            stateMachine.SetState(player.inAirState);
            return;
        }
        else if (jumpInput)
        {
            if (yInput < 0)
            {
                player.FlipX();
                if (xInput == player.facingXDirection)
                {
                    player.movement.SetVelocityX(playerData.ClimbDownXBoost * player.facingXDirection * playerData.ClimbDownFaceAwayXBoostMultiplier);
                } else
                {
                    player.movement.SetVelocityX(playerData.ClimbDownXBoost * player.facingXDirection);
                }
            }
            else
            {
                player.input.DisableGrabInputForSeconds(playerData.GrabInputDeactivationDelayAfterBounce);
                player.input.UseJumpInput();
                if (xInput != 0f && xInput != player.facingXDirection)
                {
                    player.movement.SetVelocityX(playerData.wallBounceXVelocity * -player.facingXDirection);
                }
                player.stateMachine.SetState(player.jumpState);
            }
        }
        else if (yInput == 0f)
        {
            stateMachine.SetState(player.grabState);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

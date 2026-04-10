using UnityEngine;

public class PlayerGrabState : PlayerTouchWallState
{
    public PlayerGrabState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool) : base(playerController, plrStateMachine, plrData, animBool)
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
        if (transitioned) return;
        if (!grabInput)
        {
            if (isFacingWall)
            {
                Transition(player.wallSlideState);
            }
            return;
        }
        else if (jumpInput)
        {
            player.input.DisableGrabInputForSeconds(playerData.GrabInputDeactivationDelayAfterBounce);
            player.input.UseJumpInput();
            if (xInput != 0f && xInput != player.facingXDirection)
            {
                player.movement.SetVelocityX(playerData.wallBounceXVelocity * -player.facingXDirection);
            }
            Transition(player.jumpState);
            return;
        }
        else if (yInput != 0f) {
            Transition(player.climbState);
            return;
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

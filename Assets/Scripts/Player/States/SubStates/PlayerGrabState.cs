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
        player.GravityController.DisablePlayerGravity();
        player.MovementController.ResetVelocity();
    }

    public override void Exit()
    {
        base.Exit();
        player.GravityController.SetDefaultGravityConsideringGravSwap();
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
            player.InputController.DisableGrabInputForSeconds(playerData.GrabInputDeactivationDelayAfterBounce);
            player.InputController.UseJumpInput();
            if (xInput != 0f && xInput != player.facingXDirection)
            {
                player.MovementController.SetVelocityX(playerData.wallBounceXVelocity * -player.facingXDirection);
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

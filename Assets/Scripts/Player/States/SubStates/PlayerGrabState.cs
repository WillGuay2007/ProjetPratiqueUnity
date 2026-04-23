using UnityEngine;

public class PlayerGrabState : PlayerTouchWallState
{
    public PlayerGrabState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool) : base(playerController, plrStateMachine, plrData, animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.Motor.Snap();
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.Motor.ResetGravity();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (transitioned) return;
        if (!grabInput)
        {
            if (isFacingWall)
            {
                Transition(stateMachine.wallSlideState);
            }
            return;
        }
        else if (jumpInput)
        {
            player.InputController.DisableGrabInputForSeconds(playerData.GrabInputDeactivationDelayAfterBounce);
            player.InputController.UseJumpInput();
            if (xInput != 0f && xInput != player.DirectionController.facingXDirection)
            {
                stateMachine.Motor.ApplyHorizontalImpulse(playerData.wallBounceXVelocity * -player.DirectionController.facingXDirection);
            }
            Transition(stateMachine.jumpState);
            return;
        }
        else if (yInput != 0f) {
            Transition(stateMachine.climbState);
            return;
        }

    }
}

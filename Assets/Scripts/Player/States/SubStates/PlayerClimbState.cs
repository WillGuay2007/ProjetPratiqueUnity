using UnityEngine;

public class PlayerClimbState : PlayerTouchWallState
{
    public PlayerClimbState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool) : base(playerController, plrStateMachine, plrData, animBool)
    {
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
        player.MovementController.SetVelocityX(0);
        player.MovementController.SetVelocityY(yInput * playerData.wallClimbSpeed);
        if (!grabInput)
        {
            Transition(player.inAirState);
            return;
        }
        else if (jumpInput)
        {
            if (IsClimbingDown())
            {
                ApplyBoostVelocity();
            }
            else
            {
                player.InputController.DisableGrabInputForSeconds(playerData.GrabInputDeactivationDelayAfterBounce);
                player.InputController.UseJumpInput();
                if (PlayerIsBufferingOppositeXInput())
                {
                    player.MovementController.SetVelocityX(playerData.wallBounceXVelocity * -player.facingXDirection);
                }
                Transition(player.jumpState);
            }
        }
        else if (yInput == 0f)
        {
            Transition(player.grabState);
            return;
        }
    }

    private bool PlayerIsBufferingOppositeXInput()
    {
        return xInput != 0f && xInput != player.facingXDirection;
    }

    private bool IsClimbingDown()
    {
        return yInput != player.facingYDirection;
    }

    //TO KEEP IN MIND: TRANSITION IS APPLIED THE NEXT FRAME BY THE SUPER STATE.
    private void ApplyBoostVelocity()
    {
        player.FlipX();
        if (xInput == player.facingXDirection)
        {
            player.MovementController.SetVelocityX(playerData.ClimbDownXBoost * player.facingXDirection * playerData.ClimbDownFaceAwayXBoostMultiplier);
        }
        else
        {
            player.MovementController.SetVelocityX(playerData.ClimbDownXBoost * player.facingXDirection);
        }
    }
   
}

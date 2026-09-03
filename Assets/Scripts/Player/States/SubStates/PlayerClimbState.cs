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
            Transition(stateMachine.inAirState);
            return;
        }
        else if (jumpInput)
        {
            if (IsClimbingDown()) ApplyBoostVelocity();
            else
            {
                player.InputController.DisableGrabInputForSeconds(playerData.GrabInputDeactivationDelayAfterBounce);
                player.InputController.UseJumpInput();
                if (PlayerIsBufferingOppositeXInput())
                {
                    player.MovementController.SetVelocityX(playerData.wallBounceXVelocity * -player.DirectionController.facingXDirection);
                }
                Transition(stateMachine.jumpState);
            }
        }
        else if (yInput == 0f)
        {
            Transition(stateMachine.grabState);
            return;
        }
    }

    private bool PlayerIsBufferingOppositeXInput()
    {
        return xInput != 0f && xInput != player.DirectionController.facingXDirection;
    }

    private bool IsClimbingDown()
    {
        return yInput != player.DirectionController.facingYDirection;
    }

    //TO KEEP IN MIND: TRANSITION IS APPLIED THE NEXT FRAME BY THE SUPER STATE.
    private void ApplyBoostVelocity()
    {
        player.DirectionController.FlipX();
        float boostMultiplier = (xInput == player.DirectionController.facingXDirection)
            ? playerData.ClimbDownFaceAwayXBoostMultiplier
            : 1f;
        float boostVelocity = playerData.ClimbDownXBoost * player.DirectionController.facingXDirection * boostMultiplier;

        player.MovementController.SetVelocityX(boostVelocity);
    }
   
}

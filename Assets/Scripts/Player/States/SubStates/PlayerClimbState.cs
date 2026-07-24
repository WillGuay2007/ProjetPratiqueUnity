using UnityEngine;

public class PlayerClimbState : PlayerTouchWallState
{
    public PlayerClimbState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool) : base(playerController, plrStateMachine, plrData, animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.PlrController.GravityController.DisablePlayerGravity();
        stateMachine.PlrController.MovementController.ResetVelocity();
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.PlrController.GravityController.SetDefaultGravityConsideringGravSwap();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (transitioned) return;
        stateMachine.PlrController.MovementController.SetVelocityX(0);
        stateMachine.PlrController.MovementController.SetVelocityY(yInput * playerData.wallClimbSpeed);
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
                    stateMachine.PlrController.MovementController.SetVelocityX(playerData.wallBounceXVelocity * -stateMachine.PlrController.DirectionController.facingXDirection);
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
        return xInput != 0f && xInput != stateMachine.PlrController.DirectionController.facingYDirection;
    }

    private bool IsClimbingDown()
    {
        return yInput != stateMachine.PlrController.DirectionController.facingYDirection;
    }

    //TO KEEP IN MIND: TRANSITION IS APPLIED THE NEXT FRAME BY THE SUPER STATE.
    private void ApplyBoostVelocity()
    {
        stateMachine.PlrController.DirectionController.FlipX();
        float boostMultiplier = (xInput == stateMachine.PlrController.DirectionController.facingXDirection)
            ? playerData.ClimbDownFaceAwayXBoostMultiplier
            : 1f;
        float boostVelocity = playerData.ClimbDownXBoost * stateMachine.PlrController.DirectionController.facingXDirection * boostMultiplier;

        stateMachine.PlrController.MovementController.SetVelocityX(boostVelocity);
    }
   
}

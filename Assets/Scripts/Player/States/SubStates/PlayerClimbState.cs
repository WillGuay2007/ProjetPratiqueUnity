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

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (transitioned) return;
        player.MovementController.SetVelocityX(0);
        player.MovementController.SetVelocityY(yInput * playerData.wallClimbSpeed);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (transitioned) return;
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
                player.InputController.IgnoreGrabInputForSeconds(playerData.GrabInputDeactivationDelayAfterBounce);
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

    private void ApplyBoostVelocity()
    {
        player.InputController.IgnoreJumpInputForSeconds(0.1f); //Je veut que le buffer reste actif, dont au lieu de remove le jump input, je le delay. Si je l'ignore pas, il se trigger dès la prochaine frame.
        player.InputController.IgnoreGrabInputForSeconds(playerData.GrabInputDeactivationDelayAfterBounce);
        int originalFacing = player.DirectionController.facingXDirection;
        float boostMultiplier = (xInput != originalFacing)
            ? playerData.ClimbDownFaceAwayXBoostMultiplier
            : 1f;
        float boostVelocity = playerData.ClimbDownXBoost * -originalFacing * boostMultiplier;
        player.DirectionController.FlipX();
        player.MovementController.SetVelocityX(boostVelocity);
        Transition(stateMachine.inAirState);
    }

}

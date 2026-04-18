using UnityEngine;

public class PlayerClimbState : PlayerTouchWallState
{
    public PlayerClimbState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool) : base(playerController, plrStateMachine, plrData, animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.Motor.Freeze();
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
        stateMachine.Motor.ApplyClimbVelocity(yInput, playerData.wallClimbSpeed);
        if (!grabInput)
        {
            Transition(stateMachine.inAirState);
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
                    stateMachine.Motor.ApplyHorizontalImpulse(playerData.wallBounceXVelocity * -stateMachine.Motor.GetFacingXDirection());
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
        return xInput != 0f && xInput != stateMachine.Motor.GetFacingXDirection();
    }

    private bool IsClimbingDown()
    {
        return yInput != stateMachine.Motor.GetFacingYDirection();
    }

    //TO KEEP IN MIND: TRANSITION IS APPLIED THE NEXT FRAME BY THE SUPER STATE.
    private void ApplyBoostVelocity()
    {
        stateMachine.Motor.FlipX();
        float boostMultiplier = (xInput == stateMachine.Motor.GetFacingXDirection())
            ? playerData.ClimbDownFaceAwayXBoostMultiplier
            : 1f;
        float boostVelocity = playerData.ClimbDownXBoost * stateMachine.Motor.GetFacingXDirection() * boostMultiplier;

        stateMachine.Motor.ApplyHorizontalImpulse(boostVelocity);
    }
   
}

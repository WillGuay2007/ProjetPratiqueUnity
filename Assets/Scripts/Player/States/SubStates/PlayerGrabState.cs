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
        if (!isStillOnWall) return;
        if (!grabInput)
        {
            if (isTouchingWall)
            {
                player.stateMachine.SetState(player.wallSlideState);
            }
            else
            {
                player.stateMachine.SetState(player.inAirState);
            }
            return;
        }
        if (yInput != 0f) player.stateMachine.SetState(player.climbState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

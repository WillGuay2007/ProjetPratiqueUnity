using UnityEngine;

public class PlayerWallSlideState : PlayerTouchWallState
{
    private bool m_isGrounded;
    public PlayerWallSlideState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool) : base(playerController, plrStateMachine, plrData, animBool)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        m_isGrounded = player.SurfaceDetector.IsOnGround();
    }

    public override void Enter()
    {
        base.Enter();
        player.GravityController.DisablePlayerGravity();
        player.MovementController.SetVelocityX(0);
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
        if (grabInput)
        {
            Transition(stateMachine.grabState);
            return;
        }
        else if (m_isGrounded)
        {
            Transition(stateMachine.idleState);
            return;
        }
        else if (jumpInput)
        {
            Transition(stateMachine.jumpState);
            player.InputController.IgnoreWallBounceForSeconds(0.25f);
            player.InputController.IgnoreJumpInputForSeconds(0.25f); //Sinon le joueur va trigger le jump 2 fois puisque dans le in air state, il ya un wall bounce check aussi.
            player.MovementController.SetVelocityX(playerData.wallBounceXVelocity * -player.DirectionController.facingXDirection);
            return;
        }
        else if (xInput != player.DirectionController.facingXDirection && xInput != 0f)
        {
            player.DirectionController.ShouldFlipX(xInput);
            Transition(stateMachine.inAirState);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (transitioned) return;
        player.MovementController.SetVelocityX(0);
        player.MovementController.AccelerateYVelocity(-Mathf.Abs(playerData.maxWallSlideVelocity) * player.DirectionController.facingYDirection, playerData.wallSlideFriction);
    }
}

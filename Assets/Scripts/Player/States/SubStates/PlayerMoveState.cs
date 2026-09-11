using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool) : base(playerController, plrStateMachine, plrData, animBool)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (transitioned) return;

        player.DirectionController.ShouldFlipX(xInput);

        if (xInput == 0 && player.MovementController.velocity.x == 0)
        {
            Transition(stateMachine.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (transitioned) return;
        player.MovementController.AccelerateXVelocity(playerData.movementVelocity * xInput, playerData.groundFriction);
    }
}

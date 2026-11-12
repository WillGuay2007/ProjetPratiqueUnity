using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(PlayerController playerController, PlayerStateMachine plrStateMachine, string animBool) : base(playerController, plrStateMachine, animBool)
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
        player.MovementController.AccelerateXVelocity(player.playerData.movementVelocity * xInput, player.playerData.groundFriction);
    }
}

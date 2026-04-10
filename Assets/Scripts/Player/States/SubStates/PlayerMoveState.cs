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

        player.CheckIfShouldFlipX(xInput);

        player.movement.AdjustVelocityX_Constant(playerData.movementVelocity * xInput, playerData.groundFriction);
        if (xInput == 0 && player.movement.velocity.x == 0)
        {
            Transition(player.idleState);
        }
    }
}

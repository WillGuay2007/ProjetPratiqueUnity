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

        player.MovementController.AdjustVelocityX_Constant(playerData.movementVelocity * xInput, playerData.groundFriction);
        if (xInput == 0 && player.MovementController.velocity.x == 0)
        {
            Transition(player.idleState);
        }
    }
}

using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool) : base(playerController, plrStateMachine, plrData, animBool)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (transitioned) return;

        player.MovementController.SetVelocityX(0);
        if (xInput != 0f)
        {
            Transition(player.moveState);
        }
    }
}

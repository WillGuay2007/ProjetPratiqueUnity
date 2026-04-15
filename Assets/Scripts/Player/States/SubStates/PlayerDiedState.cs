using UnityEngine;

public class PlayerDiedState : PlayerState
{
    public PlayerDiedState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool) : base(playerController, plrStateMachine, plrData, animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.MovementController.ResetVelocity();
        player.SetPhysics(false);
        //Respawn est géré grace à un animation event.
    }
}

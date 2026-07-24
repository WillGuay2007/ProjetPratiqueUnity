using UnityEngine;

public class PlayerDiedState : PlayerState
{
    public PlayerDiedState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool) : base(playerController, plrStateMachine, plrData, animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.PlrController.GravityController.DisablePlayerGravity();
        stateMachine.PlrController.MovementController.ResetVelocity();
        //Respawn est géré grace à un animation event.
    }

    public override bool CanTransitionTo(PlayerState newState) => false;
}

using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerController playerController, PlayerStateMachine plrStateMachine, string animBool) : base(playerController, plrStateMachine, animBool)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (transitioned) return;

        stateMachine.PlrController.MovementController.ResetVelocity();
        if (xInput != 0f)
        {
            Transition(stateMachine.moveState);
        }
    }
}

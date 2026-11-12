using UnityEngine;

public class PlayerAbilityState : PlayerState
{

    protected bool isAbilityDone;
    protected int xInput;
    protected int yInput;
    protected bool isGrounded;

    public PlayerAbilityState(PlayerController playerController, PlayerStateMachine plrStateMachine, string animBool) : base(playerController, plrStateMachine, animBool)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.SurfaceDetector.IsOnGround();
    }

    public override void Enter()
    {
        base.Enter();
        isAbilityDone = false;

        xInput = player.InputController.NormInputX;
        yInput = player.InputController.NormInputY;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputController.NormInputX;
        yInput = player.InputController.NormInputY;

        if (isAbilityDone)
        {
            Transition(stateMachine.inAirState);
        }

    }
}

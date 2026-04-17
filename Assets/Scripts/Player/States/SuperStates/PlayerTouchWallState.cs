using UnityEngine;

public class PlayerTouchWallState : PlayerState
{
    protected bool isFacingWall;
    private bool m_isGrounded;
    protected int xInput;
    protected int yInput;
    protected bool grabInput;
    protected bool jumpInput;
    public PlayerTouchWallState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool) : base(playerController, plrStateMachine, plrData, animBool)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isFacingWall = player.SurfaceDetector.IsFacingWall();
        m_isGrounded = player.SurfaceDetector.IsOnGround();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        grabInput = player.InputController.GrabInput;
        xInput = player.InputController.NormInputX;
        yInput = player.InputController.NormInputY;
        jumpInput = player.InputController.JumpInput;
        if (!isFacingWall)
        {
            if (m_isGrounded)
            {
                Transition(stateMachine.idleState);
            } else
            {
                Transition(stateMachine.inAirState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

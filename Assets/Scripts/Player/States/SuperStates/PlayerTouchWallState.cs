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
        isFacingWall = player.CheckIfFacingWall();
        m_isGrounded = player.CheckIfOnGround();
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
        grabInput = player.input.GrabInput;
        xInput = player.input.NormInputX;
        yInput = player.input.NormInputY;
        jumpInput = player.input.JumpInput;
        if (!isFacingWall)
        {
            if (m_isGrounded)
            {
                Transition(player.idleState);
            } else
            {
                Transition(player.inAirState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

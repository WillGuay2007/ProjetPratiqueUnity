using UnityEngine;

public class PlayerTouchWallState : PlayerState
{
    protected bool isTouchingWall;
    private bool m_isGrounded;
    protected int xInput;
    protected int yInput;
    protected bool grabInput;
    protected bool isStillOnWall;
    public PlayerTouchWallState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool) : base(playerController, plrStateMachine, plrData, animBool)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isTouchingWall = player.CheckIfIsTouchingWall();
        m_isGrounded = player.CheckIfOnGround();
    }

    public override void Enter()
    {
        base.Enter();
        isStillOnWall = true;
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
        if (!isTouchingWall)
        {
            if (m_isGrounded)
            {
                player.stateMachine.SetState(player.idleState);
            } else
            {
                player.stateMachine.SetState(player.inAirState);
            }
            isStillOnWall = false;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

using System.Collections;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    private Vector2 m_dashDirection;
    private int m_amountOfAirDashesLeft;
    private float m_elapsedTime = 0f;
    private bool m_jumpInput;
    private bool m_hasRegainedDash;
    private DashType m_dashType;

    private enum DashType
    {
        Horizontal,
        Up,
        Down,
        DiagonalUp,
        DiagonalDown,
    }

    public PlayerDashState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool) : base(playerController, plrStateMachine, plrData, animBool)
    {
        m_amountOfAirDashesLeft = playerData.amountOfAirDashes;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        StartDash();
    }

    public override void Exit()
    {
        base.Exit();
        if (player.IsFalling()) return;
        player.movement.SetVelocityY(player.movement.velocity.y * playerData.dashEndYMultiplier);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        m_jumpInput = player.input.JumpInput;
        player.CheckIfShouldFlipX(xInput);
        if (isAbilityDone) return;
        m_elapsedTime += Time.deltaTime;

        bool dashTimerEnded = m_elapsedTime > playerData.dashDuration;
        bool groundedDashRegainTimerReached = m_elapsedTime > playerData.regainDashAtDuration && m_hasRegainedDash == false;

        if (dashTimerEnded)
        {
            isAbilityDone = true;
            return;
        }
        else if (isGrounded)
        {
            if (groundedDashRegainTimerReached)
            {
                m_hasRegainedDash = true;
                ResetAmountOfDashesLeft();
            }

            //Grounded jump logic
            if (m_jumpInput)
            {
            player.input.UseJumpInput();
            isAbilityDone = true;

            if (m_dashType == DashType.Horizontal) //Superdash
            {
                player.jumpState.SetCustomJumpHeight(playerData.SuperDashYVelocity);
                player.movement.SetVelocityX(playerData.SuperDashXVelocity * player.facingXDirection);
            } else if (m_dashType == DashType.DiagonalDown) //Hyperdash
            {
                player.jumpState.SetCustomJumpHeight(playerData.HyperdashYVelocity);
                player.movement.SetVelocityX(playerData.HyperdashXVelocity * player.facingXDirection);
            }

            stateMachine.SetState(player.jumpState);
                
            return;
            }
        } 
        
        SetDirectionVelocity();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void ResetAmountOfDashesLeft() => m_amountOfAirDashesLeft = playerData.amountOfAirDashes;
    public void DecreaseAmountOfDashesLeft() => m_amountOfAirDashesLeft -= 1;
    public bool CanDash() => m_amountOfAirDashesLeft > 0;

    private Vector2 GetDirection()
    {
        float yDirection = yInput;
        float xDirection = xInput;
        Vector2 direction;

        if (m_dashType == DashType.DiagonalDown)
        {
            yDirection = yDirection * playerData.diagonalDownDashYMultiplier;
            direction = new Vector2(xDirection, yDirection).normalized;
        }
        else if (m_dashType == DashType.DiagonalUp)
        {
            yDirection = yDirection * playerData.diagonalUpDashYMultiplier;
            direction = new Vector2(xDirection, yDirection).normalized;
        }
        else if (m_dashType == DashType.Up || m_dashType == DashType.Down)
        {
            yDirection = yDirection * playerData.verticalDashYMultiplier;
            direction = new Vector2(0f, yDirection);
        }
        else
        {
            direction = new Vector2(xDirection, yDirection).normalized;
        }

        // Debug.Log(
        //     "The direction is: " + direction.ToString() +
        //     "\nThe length is: " + direction.magnitude
        // );

        return direction;
    }

    private void StartDash()
    {
        m_dashType = GetDashType();
        m_hasRegainedDash = false;
        m_elapsedTime = 0f;
        m_dashDirection = GetDirection();
        player.movement.ResetVelocity();
        SetDirectionVelocity();
        DecreaseAmountOfDashesLeft();
    }

    private void SetDirectionVelocity()
    {
        player.movement.SetVelocity(
            new Vector2(
                m_dashDirection.x * playerData.dashSpeed,
                m_dashDirection.y * playerData.dashSpeed)
            );
    }


    private DashType GetDashType()
    {
        if (xInput != 0 && yInput == 0) return DashType.Horizontal;
        if (xInput == 0 && yInput == player.facingYDirection) return DashType.Up;
        if (xInput == 0 && yInput != player.facingYDirection) return DashType.Down;
        if (xInput != 0 && yInput == player.facingYDirection) return DashType.DiagonalUp;
        if (xInput != 0 && yInput != player.facingYDirection) return DashType.DiagonalDown;
        return DashType.Horizontal;
    }
}

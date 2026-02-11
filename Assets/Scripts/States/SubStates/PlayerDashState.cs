using System.Collections;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    private Vector2 m_dashDirection;
    private int m_amountOfAirDashesLeft;
    private float m_elapsedTime = 0f;
    private bool m_jumpInput;
    private bool m_hasRegainedDash;

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
        player.SetVelocityY(player.currentVelocity.y * playerData.dashEndYMultiplier);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        m_jumpInput = player.playerInputManager.JumpInput;
        player.CheckIfShouldFlipX(xInput);
        if (isAbilityDone) return;
        m_elapsedTime += Time.deltaTime;

        bool dashTimerEnded = m_elapsedTime > playerData.dashDuration;
        bool groundedDashRegainTimerReached = m_elapsedTime > playerData.regainDashAtDuration && m_hasRegainedDash == false;
        DashType dashType = GetDashType();

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
            player.playerInputManager.UseJumpInput();
            isAbilityDone = true;

            if (dashType == DashType.Horizontal) //Superdash
            {
                player.jumpState.SetCustomJumpHeight(playerData.SuperDashYVelocity);
                player.SetVelocityX(playerData.SuperDashXVelocity * player.facingXDirection);
            } else if (dashType == DashType.DiagonalDown) //Hyperdash
            {
                player.jumpState.SetCustomJumpHeight(playerData.HyperdashYVelocity);
                player.SetVelocityX(playerData.HyperdashXVelocity * player.facingXDirection);
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

        //TODO: Fixer pour que ca marche en swap gravity.
        if (xInput != 0 && yInput < 0) /* Diagonale vers le bas */
        {
            yDirection = yDirection * playerData.diagonalDownDashYMultiplier;
            direction = new Vector2(xDirection, yDirection).normalized;
        }
        else if (xInput != 0 && yInput > 0)/* Diagonale vers le haut */
        {
            yDirection = yDirection * playerData.diagonalUpDashYMultiplier;
            direction = new Vector2(xDirection, yDirection).normalized;
        }
        else if (xInput == 0 && yInput != 0) /* Up and Down */
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
        m_hasRegainedDash = false;
        m_elapsedTime = 0f;
        m_dashDirection = GetDirection();
        player.ResetVelocity();
        SetDirectionVelocity();
        DecreaseAmountOfDashesLeft();
    }

    private void SetDirectionVelocity()
    {
        player.SetVelocity(
            new Vector2(
                m_dashDirection.x * playerData.dashSpeed,
                m_dashDirection.y * playerData.dashSpeed)
            );
    }

    private DashType GetDashType()
    {
        if (m_dashDirection.x != 0 && m_dashDirection.y == 0) return DashType.Horizontal;
        if (m_dashDirection.x == 0 && Mathf.Sign(m_dashDirection.y) == player.facingYDirection) return DashType.Up;
        if (m_dashDirection.x == 0 && Mathf.Sign(m_dashDirection.y) != player.facingYDirection) return DashType.Down;
        if (m_dashDirection.x != 0 && Mathf.Sign(m_dashDirection.y) == player.facingYDirection) return DashType.DiagonalUp;
        if (m_dashDirection.x != 0 && Mathf.Sign(m_dashDirection.y) != player.facingYDirection) return DashType.DiagonalDown;
        return DashType.Horizontal;
    }
}

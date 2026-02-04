using System.Collections;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    private Vector2 m_dashDirection;
    private int m_amountOfAirDashesLeft;
    private float m_elapsedTime = 0f;
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
        if (player.currentVelocity.y <= 0) return;
        player.SetVelocityY(player.currentVelocity.y * playerData.dashEndYMultiplier);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAbilityDone) return;
        m_elapsedTime += Time.deltaTime;
        if (m_elapsedTime > playerData.dashDuration)
        {
            m_elapsedTime = 0f;
            isAbilityDone = true;
        }
        else if (isGrounded)
        {
            player.ResetVelocity();
            isAbilityDone = true;
        }
        {
            SetDirectionVelocity();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void ResetAmountOfAirDashesLeft() => m_amountOfAirDashesLeft = playerData.amountOfAirDashes;
    public void DecreaseAmountOfDashesLeft() => m_amountOfAirDashesLeft -= 1;

    public bool CanDash()
    {
        return m_amountOfAirDashesLeft > 0;
    }

    private Vector2 GetDirection()
    {
        float yDirection = yInput;
        if (xInput != 0 && yInput < 0) yDirection = yDirection / 2; //Moins de diagonale en dash vers le bas.
        return new Vector2(xInput, yDirection).normalized;
    }

    private void StartDash()
    {
        m_elapsedTime = 0f;
        SetDirectionVelocity();
        DecreaseAmountOfDashesLeft();
        m_dashDirection = GetDirection();
        player.ResetVelocity();
    }

    private void SetDirectionVelocity()
    {
        player.SetVelocity(
            new Vector2(
                m_dashDirection.x * playerData.dashSpeed,
                m_dashDirection.y * playerData.dashSpeed)
            );
    }
}

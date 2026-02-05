using System.Collections;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    private Vector2 m_dashDirection;
    private int m_amountOfAirDashesLeft;
    private float m_elapsedTime = 0f;
    private bool m_jumpInput;
    private bool m_hasRegainedDash;
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
        if (
            player.facingYDirection == 1 && player.currentVelocity.y < 0
            || player.facingYDirection == -1 && player.currentVelocity.y > 0
            ) return;
        player.SetVelocityY(player.currentVelocity.y * playerData.dashEndYMultiplier);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        m_jumpInput = player.playerInputManager.JumpInput;
        player.CheckIfShouldFlipX(xInput);
        if (isAbilityDone) return;
        m_elapsedTime += Time.deltaTime;

        if (m_elapsedTime > playerData.dashDuration)
        {
            m_elapsedTime = 0f;
            isAbilityDone = true;
            return;
        }
        else if (isGrounded)
        {
            //Tu peut récupérer ton dash après une certaine duration, te permettant de super ou hyper dash avec un dash en plus.
            if (m_elapsedTime > playerData.regainDashAtDuration && m_hasRegainedDash == false)
            {
                m_hasRegainedDash = true;
                ResetAmountOfAirDashesLeft();
            }


                if (m_jumpInput)
                {
                player.playerInputManager.UseJumpInput();
                isAbilityDone = true;

                if (m_dashDirection.x != 0 && m_dashDirection.y == 0) //Superdash
                {
                    player.jumpState.SetCustomJumpHeight(playerData.SuperDashYVelocity);
                    player.SetVelocityX(playerData.SuperDashXVelocity * player.facingXDirection);
                } else if (m_dashDirection.x != 0 && (player.facingYDirection == 1 && m_dashDirection.y < 0)
                    || (player.facingYDirection == -1 && m_dashDirection.y > 0)
                    ) //Hyperdash
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

    public void ResetAmountOfAirDashesLeft() => m_amountOfAirDashesLeft = playerData.amountOfAirDashes;
    public void DecreaseAmountOfDashesLeft() => m_amountOfAirDashesLeft -= 1;

    public bool CanDash()
    {
        return m_amountOfAirDashesLeft > 0;
    }

    private Vector2 GetDirection()
    {
        float yDirection = yInput;
        float xDirection = xInput;
        Vector2 direction;
        if (xInput != 0 && yInput < 0) /*Diagonale vers le bas*/ {
            yDirection = yDirection * 0.5f;
            direction = new Vector2(xDirection, yDirection).normalized;
        }
        else if (xInput == 0 && yInput != 0) /*Up and Down*/ {
            yDirection = yDirection * 0.75f;
            direction = new Vector2(0f, yDirection);
        }
        else
        {
            direction = new Vector2(xDirection, yDirection).normalized;
        }
        //Debug.Log(
        //    "The direction is: " + direction.ToString() +
        //    "\nThe length is: " + direction.magnitude
        //    );
        //Enlever le commentaire pour debugger.
        return direction;
    }

    private void StartDash()
    {
        m_hasRegainedDash = false;
        m_elapsedTime = 0f;
        m_dashDirection = GetDirection();
        SetDirectionVelocity();
        DecreaseAmountOfDashesLeft();
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

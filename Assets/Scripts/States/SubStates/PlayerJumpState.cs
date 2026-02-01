using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{

    private int m_amountOfJumpsLeft;

    public PlayerJumpState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool) : base(playerController, plrStateMachine, plrData, animBool)
    {
        m_amountOfJumpsLeft = playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocityY(playerData.jumpVelocity);
        isAbilityDone = true;
        DecreaseAmountOfJumpsLeft();
        player.inAirState.SetIsJumping();
    }

    public bool CanJump()
    {
        if (m_amountOfJumpsLeft > 0)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void ResetAmoutOfJumpsLeft() => m_amountOfJumpsLeft = playerData.amountOfJumps;
    public void DecreaseAmountOfJumpsLeft() => m_amountOfJumpsLeft--;
}

using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{

    private int m_amountOfJumpsLeft;
    private float m_customJumpHeight = 0f;

    public PlayerJumpState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool) : base(playerController, plrStateMachine, plrData, animBool)
    {
        m_amountOfJumpsLeft = playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();
        AudioController.Instance.PlaySound(AudioController.Sounds.PlayerJump);
        float jumpHeight = m_customJumpHeight != 0 ? m_customJumpHeight : playerData.jumpVelocity;
        jumpHeight *= player.DirectionController.facingYDirection;
        player.MovementController.SetVelocityY(jumpHeight);
        isAbilityDone = true;
        DecreaseAmountOfJumpsLeft();
        stateMachine.inAirState.SetIsJumping();
    }

    public override void Exit()
    {
        base.Exit();
        m_customJumpHeight = 0;
    }

    public bool CanJump()
    {
        return m_amountOfJumpsLeft > 0;
    }

    public void ResetAmoutOfJumpsLeft() => m_amountOfJumpsLeft = playerData.amountOfJumps;
    public void DecreaseAmountOfJumpsLeft() => m_amountOfJumpsLeft--;
    public void IncreaseAmountOfJumpsLeft()
    {
        m_amountOfJumpsLeft++;
        Mathf.Clamp(m_amountOfJumpsLeft, 0, playerData.amountOfJumps);
    }
    public void SetCustomJumpHeight(float amount) => m_customJumpHeight = amount; //Appeler avant d'entrer dans le jump state
}

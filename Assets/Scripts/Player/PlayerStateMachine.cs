using UnityEngine;

public class PlayerStateMachine
{
    public PlayerController PlrController { get; private set; }
    public PlayerState CurrentState { get; private set; }
    public PlayerInfosWindow PlayerInfosWindow { get; private set; }
    private PlayerData m_playerData;

    #region States
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerInAirState inAirState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerDiedState diedState { get; private set; }
    public PlayerGrabState grabState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerClimbState climbState { get; private set; }
    #endregion

    public PlayerStateMachine(PlayerController plrcontroller, PlayerInfosWindow playerInfosWindow, PlayerData plrData)
    {
        GenericEventBus.Subscribe<PlayerRespawnEvent>((PlayerRespawnEvent) => ForceSetState(idleState));
        PlrController = plrcontroller;
        PlayerInfosWindow = playerInfosWindow;
        m_playerData = plrData;
    }

    public void Initialize()
    {
        idleState = new PlayerIdleState(PlrController, this, m_playerData, "Idle");
        moveState = new PlayerMoveState(PlrController, this, m_playerData, "Move");
        jumpState = new PlayerJumpState(PlrController, this, m_playerData, "InAir");
        inAirState = new PlayerInAirState(PlrController, this, m_playerData, "InAir");
        dashState = new PlayerDashState(PlrController, this, m_playerData, "Dash");
        diedState = new PlayerDiedState(PlrController, this, m_playerData, null);
        grabState = new PlayerGrabState(PlrController, this, m_playerData, "Grab");
        climbState = new PlayerClimbState(PlrController, this, m_playerData, "Climb");
        wallSlideState = new PlayerWallSlideState(PlrController, this, m_playerData, "Slide");

        CurrentState = idleState;
        CurrentState.Enter();
    }

    public void SetState(PlayerState newState)
    {
        if (newState == CurrentState) return;

        if (!CurrentState.CanTransitionTo(newState)) return;

        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

    public void ForceSetState(PlayerState newState)
    {
        if (newState == CurrentState) return;
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}

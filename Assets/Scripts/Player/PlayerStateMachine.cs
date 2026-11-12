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

    public PlayerStateMachine(PlayerController plrcontroller, PlayerInfosWindow playerInfosWindow)
    {
        GenericEventBus.Subscribe<PlayerRespawnEvent>((PlayerRespawnEvent) => ForceSetState(idleState));
        PlrController = plrcontroller;
        PlayerInfosWindow = playerInfosWindow;
    }

    public void Initialize()
    {
        idleState = new PlayerIdleState(PlrController, this, "Idle");
        moveState = new PlayerMoveState(PlrController, this, "Move");
        jumpState = new PlayerJumpState(PlrController, this, "InAir");
        inAirState = new PlayerInAirState(PlrController, this, "InAir");
        dashState = new PlayerDashState(PlrController, this, "Dash");
        diedState = new PlayerDiedState(PlrController, this, null);
        grabState = new PlayerGrabState(PlrController, this, "Grab");
        climbState = new PlayerClimbState(PlrController, this, "Climb");
        wallSlideState = new PlayerWallSlideState(PlrController, this, "Slide");

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

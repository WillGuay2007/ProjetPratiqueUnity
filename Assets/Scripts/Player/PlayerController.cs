using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData m_playerData;
    [SerializeField] private PlayerInfosWindow m_playerInfosWindow;

    #region Components
    public Rigidbody2D RigidBody {  get; private set; }
    public Animator Animator { get; private set; }
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerInputController InputController { get; private set; }
    public PlayerGravityController GravityController { get; private set; }
    public PlayerMovementController MovementController { get; private set; }
    public PlayerDirectionController DirectionController { get; private set; }
    public PlayerSurfaceDetector SurfaceDetector { get; private set; }
    public PlayerHealthController HealthController { get; private set; }
    public PlayerSpawnController SpawnController { get; private set; }
    public PlayerBoundsController BoundsController { get; private set; }
    public PlayerDeathController DeathController { get; private set; }
    public PlayerSmearFramesController SmearFramesController { get; private set; }
    #endregion
    public bool isInteracting;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        RigidBody = GetComponent<Rigidbody2D>();
        InputController = GetComponent<PlayerInputController>();
        GravityController = GetComponent<PlayerGravityController>();
        MovementController = GetComponent<PlayerMovementController>();
        DirectionController = GetComponent<PlayerDirectionController>();
        SurfaceDetector = GetComponent<PlayerSurfaceDetector>();
        HealthController = GetComponent<PlayerHealthController>();
        SpawnController = GetComponent<PlayerSpawnController>();
        BoundsController = GetComponent<PlayerBoundsController>();
        DeathController = GetComponent<PlayerDeathController>();
        SmearFramesController = GetComponent<PlayerSmearFramesController>();
    }

    private void Start()
    {
        SurfaceDetector.Initialize(m_playerData);
        DeathController.Initialize(() => StateMachine.SetState(StateMachine.diedState));
        HealthController.Initialize(m_playerData);
        //TODO: Move back new stateMachine to awake when event bus is static
        StateMachine = new PlayerStateMachine(this, m_playerInfosWindow, m_playerData);
        StateMachine.Initialize();
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }    
}
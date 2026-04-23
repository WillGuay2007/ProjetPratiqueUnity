using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData m_playerData;

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
        StateMachine = new PlayerStateMachine(this, GetComponent<PlayerMotor>(), m_playerData);
    }

    private void Start()
    {
        SurfaceDetector.Initialize(m_playerData);
        DeathController.Initialize(() => StateMachine.SetState(StateMachine.diedState));
        HealthController.Initialize(m_playerData);
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
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IDamageable
{
    #region Constants
    private int Y_LIMIT = 750;
    #endregion
    #region Components
    public Rigidbody2D rigidBody {  get; private set; }
    public Animator animator { get; private set; }
    public CapsuleCollider2D capsuleCollider { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    public PlayerInputManager input { get; private set; }
    public PlayerGravityController gravity { get; private set; }
    public PlayerMovementController movement { get; private set; }

    [SerializeField] private PlayerData m_playerData;
    #endregion
    #region Check transforms
    [SerializeField] private Transform m_groundCheck;
    #endregion
    #region State variables
    public PlayerStateMachine stateMachine { get; private set; }
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
    #region Other variables
    public int facingXDirection { get; private set; }
    public int facingYDirection { get; private set; }
    private float m_health;
    public bool isInteracting;
    public float health { get => m_health; set => m_health = value; }
    public Vector2 currentSpawnPoint { get; private set; }

    #endregion
    #region UnityCallbacks
    private void Awake()
    {
        currentSpawnPoint = transform.position;
        facingYDirection = 1;
        facingXDirection = 1;
        health = m_playerData.playerHealth;
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        input = GetComponent<PlayerInputManager>();
        gravity = GetComponent<PlayerGravityController>();
        movement = GetComponent<PlayerMovementController>();
        stateMachine = new PlayerStateMachine(this);
        idleState = new PlayerIdleState(this, stateMachine, m_playerData, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, m_playerData, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, m_playerData, "InAir");
        inAirState = new PlayerInAirState(this, stateMachine, m_playerData, "InAir");
        dashState = new PlayerDashState(this, stateMachine, m_playerData, "InAir");
        diedState = new PlayerDiedState(this, stateMachine, m_playerData, "Died");
        grabState = new PlayerGrabState(this, stateMachine, m_playerData, "Grab");
        climbState = new PlayerClimbState(this, stateMachine, m_playerData, "Climb");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, m_playerData, "Slide");
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        if (CheckIfShouldDie()) stateMachine.SetState(diedState);
        stateMachine.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }
    #endregion
    #region Other Functions
    public void FlipX()
    {
        facingXDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

    public void FlipY()
    {
        facingYDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
        transform.Rotate(0f, 0f, 180f);
    }

    public void Damage(float damage)
    {
        health -= damage;
        if (IsDead()) stateMachine.SetState(diedState);
    }

    public void Respawn()
    {
        gravity.SetDefaultGravity();
        transform.position = currentSpawnPoint;
        stateMachine.SetState(idleState);
        SetPhysics(true);
    }

    public void ClaimCheckpoint(Vector2 newSpawnPosition)
    {
        currentSpawnPoint = newSpawnPosition;
    }
    #endregion
    #region Check Functions
    public void CheckIfShouldFlipX(int xInput)
    {
        if (xInput != 0 && xInput != facingXDirection)
        {
            FlipX();
        }
    }

    public bool CheckIfOnGround()
    {
        return Physics2D.OverlapCircle(m_groundCheck.position, m_playerData.groundCheckRadius, m_playerData.whatIsGround);
    }

    public bool IsFalling()
    {
        return Mathf.Sign(movement.velocity.y) == -facingYDirection;
    }

    public bool IsGoingUp()
    {
        return Mathf.Sign(movement.velocity.y) == facingYDirection;
    }

    public bool CheckIfFacingWall()
    {
        return Physics2D.Raycast(transform.position, Vector2.right * facingXDirection, m_playerData.wallCheckRayLength, m_playerData.whatIsGround); //Ground est considéré comme un wall.
    }

    public bool CheckIfTouchingWall()
    {
        if (Physics2D.Raycast(transform.position, Vector2.right * facingXDirection, m_playerData.wallCheckRayLength, m_playerData.whatIsGround))
        {
            return true;
        }
        else if (Physics2D.Raycast(transform.position, -Vector2.right * facingXDirection, m_playerData.wallCheckRayLength, m_playerData.whatIsGround))
        {
            return true;
        }
        return false;
    }

    public bool IsDead() => health <= 0f;
    private bool CheckIfShouldDie() => Mathf.Abs(transform.position.y) > Y_LIMIT;    
    
    #endregion
    #region Set Functions
    

    public void SetPhysics(bool isEnabled)
    {
        rigidBody.simulated = isEnabled;
    }
    #endregion

}
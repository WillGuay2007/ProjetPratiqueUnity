using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Components
    public Rigidbody2D rigidBody {  get; private set; }
    public Animator animator { get; private set; }
    public CapsuleCollider2D capsuleCollider { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    public PlayerInputManager playerInputManager { get; private set; }
    public PlayerGravityController playergravityController { get; private set; }

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
    #endregion
    #region Other variables
    public int facingXDirection { get; private set; }
    public int facingYDirection { get; private set; }
    public Vector2 currentVelocity;
    #endregion
    #region UnityCallbacks
    private void Awake()
    {
        facingYDirection = 1;
        facingXDirection = 1;
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        playerInputManager = GetComponent<PlayerInputManager>();
        playergravityController = GetComponent<PlayerGravityController>();
        currentVelocity = rigidBody.linearVelocity;
        stateMachine = new PlayerStateMachine(this);
        idleState = new PlayerIdleState(this, stateMachine, m_playerData, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, m_playerData, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, m_playerData, "InAir");
        inAirState = new PlayerInAirState(this, stateMachine, m_playerData, "InAir");
        dashState = new PlayerDashState(this, stateMachine, m_playerData, "InAir");
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        currentVelocity = rigidBody.linearVelocity;
        stateMachine.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }
    #endregion
    #region Other Functions
    private void FlipX()
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
    #endregion
    #region Set Functions
    public void SetVelocityX(float velocity)
    {
        rigidBody.linearVelocityX = velocity;
    }

    public void SetVelocityY(float velocity) 
    {
        rigidBody.linearVelocityY = velocity;
    }

    public void ResetVelocity() => rigidBody.linearVelocity = Vector2.zero;

    public void SetVelocity(Vector2 velocity)
    {
        rigidBody.linearVelocity = velocity;
    }

    public void AdjustVelocityX_Lerp(float velocity, float friction)
    {
        if (friction > 1) friction = 1;
        float targetVelocityX = Mathf.Lerp(currentVelocity.x, velocity, friction);
        SetVelocityX(targetVelocityX);
    }

    public void AdjustVelocityY_Lerp(float velocity, float friction)
    {
        if (friction > 1) friction = 1;
        float targetVelocityY = Mathf.Lerp(currentVelocity.y, velocity, friction);
        SetVelocityX(targetVelocityY);
    }

    public void AdjustVelocityX_Constant(float velocity, float interval)
    {
        if (currentVelocity.x > velocity) interval *= -1;
        float newVelocity = currentVelocity.x + interval;
        if (interval < 0)
        {
            if (newVelocity < velocity) newVelocity = velocity;
        } else
        {
            if (newVelocity > velocity) newVelocity = velocity;
        }
            SetVelocityX(newVelocity);
    }

    public void AdjustVelocityY_Constant(float velocity, float interval)
    {
        if (currentVelocity.x > velocity) interval *= -1;
        float newVelocity = currentVelocity.y + interval;
        SetVelocityY(newVelocity);
    }

    #endregion
}

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
    #endregion
    #region Other variables
    public int facingDirection { get; private set; }
    public Vector2 currentVelocity;
    #endregion
    #region UnityCallbacks
    private void Awake()
    {
        facingDirection = 1;
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        playerInputManager = GetComponent<PlayerInputManager>();
        currentVelocity = rigidBody.linearVelocity;
        stateMachine = new PlayerStateMachine(this);
        idleState = new PlayerIdleState(this, stateMachine, m_playerData, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, m_playerData, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, m_playerData, "Jump");
        inAirState = new PlayerInAirState(this, stateMachine, m_playerData, "Jump");
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
    private void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }
    #endregion
    #region Check Functions
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != facingDirection)
        {
            Flip();
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
    #endregion
}

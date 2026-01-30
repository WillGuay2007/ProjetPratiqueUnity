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
    #region State variables
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerState idleState { get; private set; }
    public PlayerState moveState { get; private set; }
    #endregion
    #region Other variables
    public bool isGrounded { get; private set; }
    public int facingDirection { get; private set; }
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
        stateMachine = new PlayerStateMachine(this);
        idleState = new PlayerIdleState(this, stateMachine, m_playerData, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, m_playerData, "Move");
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
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

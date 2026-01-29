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
    [SerializeField] private PlayerData m_playerData;
    #endregion
    #region StateMachine
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerInputManager playerInputManager { get; private set; }
    public PlayerState idleState { get; private set; }
    public PlayerState moveState { get; private set; }
    #endregion
    #region Bools
    public bool isGrounded { get; private set; }
    #endregion

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
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

    public void SetVelocityX(float x)
    {

    }

    public void SetVelocityY(float y) {
    
    }
}

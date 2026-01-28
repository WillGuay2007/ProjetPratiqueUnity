using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public enum PlayerStates {
        Idling,
        Walking,
        Running,
        Jumping,
        Falling,
        Attacking,
        Crouching,
    }


    private Rigidbody2D m_RigidBody;
    private Animator m_Animator;
    private CapsuleCollider2D m_CapsuleCollider;
    private SpriteRenderer m_SpriteRenderer;
    private PlayerStates m_CurrentState;
    private PlayerStateMachine m_StateMachine;
    public PlayerStates CurrentState
    {
        get { return m_CurrentState; }
    }
    

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_CapsuleCollider = GetComponent<CapsuleCollider2D>();
        m_CurrentState = PlayerStates.Idling;
    }

    private void FixedUpdate()
    {
        
    }
}

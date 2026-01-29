using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D m_RigidBody;
    private Animator m_Animator;
    private CapsuleCollider2D m_CapsuleCollider;
    private SpriteRenderer m_SpriteRenderer;
    private StateMachine<PlayerController> m_StateMachine;


    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_CapsuleCollider = GetComponent<CapsuleCollider2D>();
        m_StateMachine = new StateMachine<PlayerController>(this);
        //TODO: Set default state
    }

    private void FixedUpdate()
    {
        m_StateMachine.Update();
    }
}

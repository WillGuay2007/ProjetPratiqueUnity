using UnityEngine;

public class PlayerGravityController : MonoBehaviour
{
    private PlayerController m_player;
    private Rigidbody2D m_rigidbody;
    private float m_originalPlayerGravity;

    private void Start()
    {
        m_player = GetComponent<PlayerController>();
        m_originalPlayerGravity = m_player.rigidBody.gravityScale;
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetPlayerGravity(float newPlayerGravity)
    {
        m_rigidbody.gravityScale = newPlayerGravity;
    }

    public void SetDefaultGravity()
    {
        m_rigidbody.gravityScale = m_originalPlayerGravity;
        if (m_player.facingYDirection == 1) return;
        m_player.FlipY();
    }

    public void SetDefaultGravityConsideringGravSwap()
    {
        m_rigidbody.gravityScale = m_originalPlayerGravity * m_player.facingYDirection;
    }

    public void DisablePlayerGravity()
    {
        m_rigidbody.gravityScale = 0f;
    }

    public void SwapPlayerGravity()
    {
        m_rigidbody.gravityScale *= -1;
        m_player.FlipY();
    }
}

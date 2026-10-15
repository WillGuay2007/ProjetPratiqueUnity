using UnityEngine;

public class PlayerDirectionController : MonoBehaviour
{
    private const float REVERSED_COLLIDER_Y_OFFSET = 0.192f;
    private const float COLLIDER_Y_OFFSET = -0.165f;
    private SpriteRenderer m_playerSpriteRenderer;
    private CapsuleCollider2D m_playerCollider;
    [SerializeField] private Transform m_playerGroundCheck;

    private void Awake()
    {
        m_playerSpriteRenderer = GetComponent<SpriteRenderer>();
        m_playerCollider = GetComponent<CapsuleCollider2D>();
    }

    public int facingXDirection { get; private set; } = 1;
    public int facingYDirection { get; private set; } = 1;

    public void ShouldFlipX(int xInput)
    {
        if (xInput != 0 && xInput != facingXDirection) FlipX();
    }
    public int FlipX()
    {
        facingXDirection *= -1;
        m_playerSpriteRenderer.flipX = !m_playerSpriteRenderer.flipX;
        return facingXDirection;
    }

    public int FlipY()
    {
        facingYDirection *= -1;
        m_playerSpriteRenderer.flipY = !m_playerSpriteRenderer.flipY;
        m_playerCollider.offset = new Vector2(0f, m_playerSpriteRenderer.flipY ? REVERSED_COLLIDER_Y_OFFSET : COLLIDER_Y_OFFSET);
        m_playerGroundCheck.localPosition = new Vector3(0f, 1.1f * -facingYDirection, 0f);
        return facingYDirection;
    }
}

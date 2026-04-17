using UnityEngine;

public class PlayerSurfaceDetector : MonoBehaviour
{
    [SerializeField] private Transform m_groundCheck;
    private PlayerDirectionController m_directionController;
    private PlayerData m_playerData;

    private void Awake()
    {
        m_directionController = GetComponent<PlayerDirectionController>();
    }

    public void Initialize(PlayerData plrData)
    {
        m_playerData = plrData;
    }

    public bool IsOnGround()
    {
        return Physics2D.OverlapCircle(m_groundCheck.position, m_playerData.groundCheckRadius, m_playerData.whatIsGround);
    }

    public bool IsFacingWall()
    {
        return Physics2D.Raycast(transform.position, Vector2.right * m_directionController.facingXDirection, m_playerData.wallCheckRayLength, m_playerData.whatIsGround); //Ground est considéré comme un wall.
    }

    public bool IsTouchingWall()
    {
        return IsFacingWall() || Physics2D.Raycast(transform.position, -Vector2.right * m_directionController.facingXDirection, m_playerData.wallCheckRayLength, m_playerData.whatIsGround);
    }
}

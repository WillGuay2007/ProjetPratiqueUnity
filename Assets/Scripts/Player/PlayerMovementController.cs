using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public Vector2 velocity => m_rb.linearVelocity;
    private Rigidbody2D m_rb;
    private PlayerDirectionController m_directionController;
    private void Awake()
    {
        m_directionController = GetComponent<PlayerDirectionController>();
        m_rb = GetComponent<Rigidbody2D>();
    }

    public void SetVelocityX(float velocity)
    {
        m_rb.linearVelocityX = velocity;
    }

    public void SetVelocityY(float velocity)
    {
        m_rb.linearVelocityY = velocity;
    }

    public void ResetVelocity() => m_rb.linearVelocity = Vector2.zero;

    public void SetVelocity(Vector2 velocity)
    {
        m_rb.linearVelocity = velocity;
    }

    private float AdjustVelocity(float current, float target, float interval)
    {
        interval = Mathf.Abs(interval);
        if (current == target) return current;
        if (current > target) interval *= -1;
        float newVelocity = current + interval;
        if (interval < 0)
            return Mathf.Max(newVelocity, target);
        else
            return Mathf.Min(newVelocity, target);
    }

    public void AccelerateXVelocity(float velocity, float interval) => SetVelocityX(AdjustVelocity(m_rb.linearVelocityX, velocity, interval));

    public void AccelerateYVelocity(float velocity, float interval) => SetVelocityY(AdjustVelocity(m_rb.linearVelocityY, velocity, interval));

    public bool IsFalling()
    {
        return Mathf.Sign(velocity.y) == -m_directionController.facingYDirection;
    }

    public bool IsGoingUp()
    {
        return Mathf.Sign(velocity.y) == m_directionController.facingYDirection;
    }
}

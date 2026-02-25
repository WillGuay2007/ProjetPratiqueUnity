using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private Rigidbody2D m_rb;
    public Vector2 velocity => m_rb.linearVelocity;
    private void Awake()
    {
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

    public void AdjustVelocityX_Lerp(float velocity, float friction)
    {
        if (friction > 1) friction = 1;
        float targetVelocityX = Mathf.Lerp(m_rb.linearVelocityX, velocity, friction);
        SetVelocityX(targetVelocityX);
    }

    public void AdjustVelocityY_Lerp(float velocity, float friction)
    {
        if (friction > 1) friction = 1;
        float targetVelocityY = Mathf.Lerp(m_rb.linearVelocityY, velocity, friction);
        SetVelocityY(targetVelocityY);
    }

    public void AdjustVelocityX_Constant(float velocity, float interval)
    {
        if (m_rb.linearVelocityX == velocity) return;

        if (m_rb.linearVelocityX > velocity) interval *= -1;

        float newVelocity = m_rb.linearVelocityX + interval;

        if (interval < 0)
        {
            if (newVelocity < velocity) newVelocity = velocity;
        }
        else
        {
            if (newVelocity > velocity) newVelocity = velocity;
        }
        SetVelocityX(newVelocity);
    }

    public void AdjustVelocityY_Constant(float velocity, float interval)
    {
        if (m_rb.linearVelocityY == velocity) return;

        if (m_rb.linearVelocityY > velocity) interval *= -1;

        float newVelocity = m_rb.linearVelocityY + interval;

        if (interval < 0)
        {
            if (newVelocity < velocity) newVelocity = velocity;
        }
        else
        {
            if (newVelocity > velocity) newVelocity = velocity;
        }

        SetVelocityY(newVelocity);
    }
}

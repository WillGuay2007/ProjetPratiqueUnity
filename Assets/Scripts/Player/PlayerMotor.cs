using UnityEngine;

public class PlayerMotor : MonoBehaviour, IMotor
{
    private PlayerGravityController m_gravityController;
    private PlayerMovementController m_movementController;
    private PlayerDirectionController m_directionController;

    private void Awake()
    {
        m_gravityController = GetComponent<PlayerGravityController>();
        m_movementController = GetComponent<PlayerMovementController>();
        m_directionController = GetComponent<PlayerDirectionController>();
    }
    public void Dash()
    {
        throw new System.NotImplementedException();
    }

    public void Snap()
    {
        m_gravityController.DisablePlayerGravity();
        m_movementController.ResetVelocity();
    }
    public void ApplyClimbVelocity(int yInput, float velocity)
    {
        m_movementController.SetVelocityX(0);
        m_movementController.SetVelocityY(yInput * velocity);
    }
    public void ResetVelocity() => m_movementController.ResetVelocity();
    public void ResetGravity() => m_gravityController.SetDefaultGravityConsideringGravSwap();
    public void ApplyHorizontalImpulse(float v) { m_movementController.SetVelocityX(v); }
    public void ApplyVerticalImpulse(float v) { m_movementController.SetVelocityY(v); }
    public int GetFacingXDirection() => m_directionController.facingXDirection;
    public int GetFacingYDirection() => m_directionController.facingYDirection;
    public int FlipX() => m_directionController.FlipX();
    public int FlipY() => m_directionController.FlipY();
}

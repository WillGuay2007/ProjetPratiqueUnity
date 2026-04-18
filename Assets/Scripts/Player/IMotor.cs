using UnityEngine;

public interface IMotor
{
    public void ApplyClimbVelocity(int yDirection, float velocity);
    public void Dash();
    public void Freeze();
    public void ResetGravity();
    public void ApplyHorizontalImpulse(float velocity);
    public void ApplyVerticalImpulse(float velocity);
    public int GetFacingXDirection();
    public int GetFacingYDirection();
    public int FlipX();
    public int FlipY();
}

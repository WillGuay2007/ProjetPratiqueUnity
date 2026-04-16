using UnityEngine;

public class CharacterState : MonoBehaviour
{
    //TODO: Link with player controller and remove old variables from player controller once completed.
    public bool IsGrounded { get ; private set; } = true;
    public int FacingXDirection { get; private set; } = 1;
    public int FacingYDirection { get; private set; } = 1;

    public void SetFacingXDirection(int newFacingXDirection)
    {
        if (Mathf.Abs(newFacingXDirection) != 1)
        {
            print("FacingXDirection must be either 1 or -1");
        }
        FacingXDirection = newFacingXDirection;
    }
    public void SetFacingYDirection(int newFacingYDirection)
    {
        if (Mathf.Abs(newFacingYDirection) != 1)
        {
            print("FacingYDirection must be either 1 or -1");
        }
        FacingYDirection = newFacingYDirection;
    }
    public void SetIsGrounded(bool newIsGrounded)
    {
        IsGrounded = newIsGrounded;
    }
}

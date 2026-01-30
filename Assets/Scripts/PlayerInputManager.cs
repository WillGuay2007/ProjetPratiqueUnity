using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public Vector2 RawMovementInput {  get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }

    public void OnMove(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();
        NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormInputY = (int)(RawMovementInput* Vector2.up).normalized.y;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        
    }
}

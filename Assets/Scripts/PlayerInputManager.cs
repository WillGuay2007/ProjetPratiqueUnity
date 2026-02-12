using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public Vector2 RawMovementInput {  get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool DashInput { get; private set; }
    public bool InteractInput { get; private set; }
    public bool SwapGravityInput { get; private set; }

    [SerializeField] private float m_jumpInputHoldTime = 0.2f;
    [SerializeField] private float m_dashBuffer = 0.2f;
    private float m_jumpInputStartTime;
    private float m_dashInputStartTime;

    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();
        NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormInputY = (int)(RawMovementInput* Vector2.up).normalized.y;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            m_jumpInputStartTime = Time.time;
        }

        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            m_dashInputStartTime = Time.time;
            DashInput = true;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            InteractInput = true;
        }
        else if (context.canceled)
        {
            InteractInput = false;
        }
    }

    public void OnSwapGravity(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SwapGravityInput = true;
        }
        else if (context.canceled)
        {
            SwapGravityInput = false;
        }
    }

    public void UseJumpInput() => JumpInput = false;
    public void UseDashInput() => DashInput = false;
    public void UseInteractInput() => InteractInput = false;
    public void UseSwapGravityInput() => SwapGravityInput = false;
    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= m_jumpInputStartTime + m_jumpInputHoldTime)
        {
            JumpInput = false;
        }
    }
    private void CheckDashInputHoldTime()
    {
        if (Time.time >= m_dashInputStartTime + m_dashBuffer)
        {
            DashInput = false;
        }
    }

    //TODO: Add disable input
}

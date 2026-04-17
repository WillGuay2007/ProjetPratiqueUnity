using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

//TODO: Refactor la logique pas propre, donc tout.
public class PlayerInputController : MonoBehaviour
{
    public Vector2 RawMovementInput {  get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool DashInput { get; private set; }
    public bool InteractInput { get; private set; }
    public bool GrabInput { get; private set; }
    public bool SwapGravityInput { get; private set; }
    private bool m_isInputEnabled = true;
    private bool m_isHoldingGrab;
    [SerializeField] private float m_jumpInputHoldTime = 0.2f;
    [SerializeField] private float m_dashBuffer = 0.2f;
    private float m_jumpInputStartTime;
    private float m_dashInputStartTime;

    private void Update()
    {
        if (!m_isInputEnabled) return;
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
    }

    public void SetInputEnabled(bool enabled)
    {
        if (enabled)
        {
            m_isInputEnabled = true;
        }
        else
        {
            ClearInputs();
            m_isInputEnabled = false;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!m_isInputEnabled) return;
        RawMovementInput = context.ReadValue<Vector2>();
        NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormInputY = (int)(RawMovementInput* Vector2.up).normalized.y;
    }

    public void OnGrab(InputAction.CallbackContext context)
    {
        if (!m_isInputEnabled) return;
        if (context.started)
        {
            m_isHoldingGrab = true;
            GrabInput = true;
        }
        else if (context.canceled)
        {
            m_isHoldingGrab = false;
            GrabInput = false;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!m_isInputEnabled) return;
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
        if (!m_isInputEnabled) return;
        if (context.started)
        {
            m_dashInputStartTime = Time.time;
            DashInput = true;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!m_isInputEnabled) return;
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
        if (!m_isInputEnabled) return;
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
    public void DisableGrabInputForSeconds(float seconds) => StartCoroutine(DisableGrabInput(seconds));
    private IEnumerator DisableGrabInput(float seconds)
    {
        GrabInput = false;
        yield return new WaitForSeconds(seconds);
        if (m_isHoldingGrab) GrabInput = true;
    }
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

    private void ClearInputs()
    {
        JumpInput = false;
        DashInput = false;
        InteractInput = false;
        SwapGravityInput = false;
        RawMovementInput = Vector2.zero;
        NormInputX = 0;
        NormInputY = 0;
    }

}

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private struct BufferedInput
    {
        private bool m_active;
        private float m_stopBufferAt;
        private float m_ignoreInputUntil;

        public void Set(float duration)
        {
            m_active = true;
            m_stopBufferAt = Time.time + duration;
        }

        public void UseInput() => m_active = false;

        public void IgnoreInputForSeconds(float duration) => m_ignoreInputUntil = Time.time + duration;

        public void Clear()
        {
            m_active = false;
            m_ignoreInputUntil = 0f;
        }

        public bool IsValid() => m_active && Time.time < m_stopBufferAt && Time.time > m_ignoreInputUntil;
    }

    private struct HeldInput
    {
        private bool m_held;
        private float m_ignoreInputUntil;

        public void Press() => m_held = true;
        public void Release() => m_held = false;
        public void IgnoreInputForSeconds(float duration) => m_ignoreInputUntil = Time.time + duration;
        public void Clear() => m_ignoreInputUntil = 0f;
        public bool IsValid() => m_held && Time.time > m_ignoreInputUntil;
    }

    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }

    public bool JumpInput => m_jumpInput.IsValid();
    public bool JumpInputStop { get; private set; }
    public bool DashInput => m_dashInput.IsValid();
    public bool InteractInput => m_interactInput.IsValid();
    public bool SwapGravityInput => m_swapGravityInput.IsValid();
    public bool GrabInput => m_grabInput.IsValid();

    public void UseJumpInput() => m_jumpInput.UseInput();
    public void UseDashInput() => m_dashInput.UseInput();
    public void UseInteractInput() => m_interactInput.UseInput();
    public void UseSwapGravityInput() => m_swapGravityInput.UseInput();
    public void IgnoreGrabInputForSeconds(float seconds) => m_grabInput.IgnoreInputForSeconds(seconds);
    public void IgnoreWallBounceForSeconds(float seconds) => m_wallBounce.IgnoreInputForSeconds(seconds);
    public void IgnoreJumpInputForSeconds(float seconds) => m_jumpInput.IgnoreInputForSeconds(seconds);

    public bool WallBounceAllowed => m_wallBounce.IsValid();

    [SerializeField] private float m_jumpBufferDuration = 0.2f;
    [SerializeField] private float m_dashBufferDuration = 0.2f;

    private BufferedInput m_jumpInput;
    private BufferedInput m_dashInput;
    private BufferedInput m_interactInput;
    private BufferedInput m_swapGravityInput;

    private HeldInput m_grabInput;
    private HeldInput m_wallBounce;

    private bool m_isInputEnabled = true;

    public void SetInputEnabled(bool enabled)
    {
        m_isInputEnabled = enabled;
        if (!enabled) ClearAllInputs();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!m_isInputEnabled) return;
        RawMovementInput = context.ReadValue<Vector2>();
        NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!m_isInputEnabled) return;
        if (context.started)
        {
            m_jumpInput.Set(m_jumpBufferDuration);
            JumpInputStop = false;
        }
        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (!m_isInputEnabled) return;
        if (context.started) m_dashInput.Set(m_dashBufferDuration);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!m_isInputEnabled) return;
        if (context.started) m_interactInput.Set(float.MaxValue);
        if (context.canceled) m_interactInput.UseInput();
    }

    public void OnSwapGravity(InputAction.CallbackContext context)
    {
        if (!m_isInputEnabled) return;
        if (context.started) m_swapGravityInput.Set(float.MaxValue);
        if (context.canceled) m_swapGravityInput.UseInput();
    }

    public void OnGrab(InputAction.CallbackContext context)
    {
        if (!m_isInputEnabled) return;
        if (context.started) m_grabInput.Press();
        if (context.canceled) m_grabInput.Release();
    }

    private void ClearAllInputs()
    {
        m_jumpInput.Clear();
        m_dashInput.Clear();
        m_interactInput.Clear();
        m_swapGravityInput.Clear();
        m_grabInput.Clear();
        RawMovementInput = Vector2.zero;
        NormInputX = 0;
        NormInputY = 0;
        JumpInputStop = false;
    }
}
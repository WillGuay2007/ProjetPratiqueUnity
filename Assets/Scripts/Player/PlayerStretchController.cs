using UnityEngine;

public class PlayerStretchController : MonoBehaviour
{
    private const float MAX_STRETCH = .5f;
    private const float MAX_SPEED_FOR_FULL_STRETCH = 75f;
    [SerializeField] private Transform m_playerVisuals;
    private Rigidbody2D m_playerRigidBody;

    private Vector3 GetStretchTargetScale()
    {
        float ySpeed = Mathf.Abs(m_playerRigidBody.linearVelocity.y);
        float speedFraction = Mathf.Clamp01(ySpeed / MAX_SPEED_FOR_FULL_STRETCH);
        float stretch = speedFraction * MAX_STRETCH;
        return new Vector3(1 - stretch, 1 + stretch, 1);
    }

    private void Awake()
    {
        m_playerRigidBody = GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        m_playerVisuals.localScale = Vector3.Lerp(m_playerVisuals.transform.localScale, GetStretchTargetScale(), Time.deltaTime * 10);
    }
}

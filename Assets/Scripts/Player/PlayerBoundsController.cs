using UnityEngine;
public class PlayerBoundsController : MonoBehaviour
{
    private const float DEFAULT_LIMIT = 400f;
    private float m_upperLimit;
    private float m_lowerLimit;
    private PlayerDeathController m_deathController;

    private void Awake()
    {
        m_deathController = GetComponent<PlayerDeathController>();
        SetBoundsRelativeToPlayer();
    }

    public void SetBoundsRelativeToPlayer()
    {
        m_upperLimit = transform.position.y + DEFAULT_LIMIT;
        m_lowerLimit = transform.position.y - DEFAULT_LIMIT;
    }

    public bool IsOutOfBounds() => transform.position.y > m_upperLimit || transform.position.y < m_lowerLimit;

    private void Update()
    {
        if (IsOutOfBounds()) m_deathController.Die();
    }
}
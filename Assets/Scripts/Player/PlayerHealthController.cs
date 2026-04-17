using UnityEngine;
public class PlayerHealthController : MonoBehaviour, IDamageable
{
    private float m_health;
    private bool m_isDead;
    private PlayerData m_playerData;
    private PlayerDeathController m_deathController;

    private void Awake()
    {
        m_deathController = GetComponent<PlayerDeathController>();
    }

    public void Initialize(PlayerData playerData)
    {
        m_playerData = playerData;
        m_health = playerData.playerHealth;
    }

    public void Damage(float damage)
    {
        if (m_isDead) return;
        m_health -= damage;
        if (IsDead())
        {
            m_isDead = true;
            m_deathController.Die();
        }
    }

    public void Kill() => Damage(m_health);
    public bool IsDead() => m_health <= 0f;

    public void Reset()
    {
        m_health = m_playerData.playerHealth;
        m_isDead = false;
    }
}
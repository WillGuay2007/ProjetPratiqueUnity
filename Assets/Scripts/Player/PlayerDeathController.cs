using System.Collections;
using UnityEngine;
public class PlayerDeathController : MonoBehaviour
{
    private const float RESPAWN_TIME = 0.5f;
    [SerializeField] CameraController m_cameraController;
    [SerializeField] GameObject m_deathBurstParticles;
    [SerializeField] SpriteRenderer m_playerSpriteRenderer;
    private PlayerController m_player;
    private bool m_isDying = false;

    private void Awake() => m_player = GetComponent<PlayerController>();

    public void Die()
    {
        if (m_isDying) return;
        m_isDying = true;
        m_player.StateMachine.SetState(m_player.StateMachine.diedState);
        m_playerSpriteRenderer.enabled = false;
        Instantiate(m_deathBurstParticles, m_player.transform.position, Quaternion.identity);
        AudioController.Instance.PlaySound(AudioController.Sounds.PlayerDeath);
        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(RESPAWN_TIME);
        m_player.SpawnController.Respawn();
        m_playerSpriteRenderer.enabled = true;
        m_isDying = false;
    }
}
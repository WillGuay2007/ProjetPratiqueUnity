using System;
using UnityEngine;

public class PlayerSpawnController : MonoBehaviour
{
    private ISpawnProvider m_spawnProvider;
    private PlayerGravityController m_gravityController;
    private PlayerHealthController m_healthController;
    private Action m_onRespawn;

    public void Initialize(Action onRespawn) => m_onRespawn = onRespawn;

    private void Awake()
    {
        m_spawnProvider = GetComponent<ISpawnProvider>();
        if (m_spawnProvider == null) Debug.LogError("No ISpawnProvider found on the player.");
        m_gravityController = GetComponent<PlayerGravityController>();
        m_healthController = GetComponent<PlayerHealthController>();
    }

    public void SetSpawnProvider(ISpawnProvider spawnProvider) => m_spawnProvider = spawnProvider;

    public void Respawn()
    {
        m_gravityController.SetDefaultGravity();
        transform.position = m_spawnProvider.ProvideSpawn();
        m_gravityController.SetPhysics(true);
        m_healthController.Reset();
        m_onRespawn?.Invoke();
    }
}

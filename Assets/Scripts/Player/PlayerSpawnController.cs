using System;
using UnityEngine;

public class PlayerSpawnController : MonoBehaviour
{
    private ISpawnProvider m_spawnProvider;

    private void Awake()
    {
        m_spawnProvider = GetComponent<ISpawnProvider>();
        if (m_spawnProvider == null) Debug.LogError("No ISpawnProvider found on the player.");
    }

    private void TeleportToSpawn() => transform.position = m_spawnProvider.ProvideSpawn();

    public void SetSpawnProvider(ISpawnProvider spawnProvider) => m_spawnProvider = spawnProvider;

    public void Respawn()
    {
        TeleportToSpawn();
        GenericEventBus.Publish(new PlayerRespawnEvent(m_spawnProvider.ProvideSpawn()));
    }
}

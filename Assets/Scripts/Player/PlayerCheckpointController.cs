using UnityEngine;

//Cette classe a été concue pour être attachée au player. Sinon, ca marche pas.
public class PlayerCheckpointController : MonoBehaviour, ISpawnProvider
{
    private Vector2 m_checkpointSpawn;
    private int m_currentCheckpointIndex = 0;

    [SerializeField] private MonoBehaviour m_notificationService;
    private INotificationService NotificationService => m_notificationService as INotificationService;

    private void Awake() => m_checkpointSpawn = transform.position;
    public Vector2 ProvideSpawn()
    {
        return m_checkpointSpawn;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<PlayerCheckpoint>(out PlayerCheckpoint checkpoint)) return;
        if (!ShouldProcessCheckpoint(checkpoint)) return;

        HandleCheckpointReached(checkpoint);
    }

    private void HandleCheckpointReached(PlayerCheckpoint checkpoint)
    {
        checkpoint.Claim();
        UpdateCurrentCheckpoint(checkpoint);
        NotificationService.Notify("Player reached checkpoint " + m_currentCheckpointIndex);
    }

    private void UpdateCurrentCheckpoint(PlayerCheckpoint checkpoint)
    {
        m_currentCheckpointIndex = checkpoint.CheckpointIndex;
        m_checkpointSpawn = checkpoint.transform.position;
    }

    private bool ShouldProcessCheckpoint(PlayerCheckpoint checkpoint)
    {
        if (checkpoint.IsClaimed) return false;
        if (!IsNextCheckpoint(checkpoint)) return false;
        if (IsCheckpointSkipped(checkpoint))
        {
            NotificationService.Notify("Please go back. You skipped a checkpoint.");
            return false;
        }

        return true;
    }

    private bool IsCheckpointSkipped(PlayerCheckpoint checkpoint)
    {
        return checkpoint.CheckpointIndex > m_currentCheckpointIndex + 1;
    }

    private bool IsNextCheckpoint(PlayerCheckpoint checkpoint)
    {
        return checkpoint.CheckpointIndex - 1 == m_currentCheckpointIndex;
    }
}

using UnityEngine;

//Cette classe a été concue pour être attachée au player. Sinon, ca marche pas.
public class PlayerCheckpointController : MonoBehaviour
{
    public Vector2 CheckpointSpawn { get; private set; }
    private int CurrentCheckpointIndex = 0;

    [SerializeField] private MonoBehaviour m_notificationService;
    private INotificationService NotificationService => m_notificationService as INotificationService;

    public void SetDefaultSpawnPosition(Vector2 spawnPosition)
    {
        CheckpointSpawn = spawnPosition;
    }

    //Fonction root de logique.
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
        NotificationService.Notify("Player reached checkpoint " + CurrentCheckpointIndex);
    }

    private void UpdateCurrentCheckpoint(PlayerCheckpoint checkpoint)
    {
        CurrentCheckpointIndex = checkpoint.CheckpointIndex;
        CheckpointSpawn = checkpoint.transform.position;
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
        return checkpoint.CheckpointIndex > CurrentCheckpointIndex + 1;
    }

    private bool IsNextCheckpoint(PlayerCheckpoint checkpoint)
    {
        return checkpoint.CheckpointIndex - 1 == CurrentCheckpointIndex;
    }
}

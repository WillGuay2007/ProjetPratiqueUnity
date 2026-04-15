using UnityEngine;

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

    private void SetCheckpoint(PlayerCheckpoint checkpoint)
    {
        checkpoint.Claim();
        CurrentCheckpointIndex = checkpoint.CheckpointIndex;
        CheckpointSpawn = checkpoint.transform.position;
        NotificationService.Notify("Player reached checkpoint " + CurrentCheckpointIndex);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<PlayerCheckpoint>(out PlayerCheckpoint checkpoint)) return;
        if (!(checkpoint.CheckpointIndex - 1 == CurrentCheckpointIndex)) { 
            if (checkpoint.CheckpointIndex > CurrentCheckpointIndex + 1) {
                NotificationService.Notify("Please go back. You skipped a checkpoint.");
            }
            return; 
        }
        if (checkpoint.IsClaimed == true) return;
        SetCheckpoint(checkpoint);
    }
}

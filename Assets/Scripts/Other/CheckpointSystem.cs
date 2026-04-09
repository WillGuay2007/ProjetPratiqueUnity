using Unity.VisualScripting;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{

    [SerializeField] private MonoBehaviour m_notificationService;
    private INotificationService NotificationService => m_notificationService as INotificationService;

    private bool isClaimed;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isClaimed) return;
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            ClaimCheckpoint();
            NotificationService.Notify("Reached checkpoint " + name.Filter(false, true) + "!");
            player.ClaimCheckpoint(transform.position);
        }
    }

    private void ClaimCheckpoint()
    {
        isClaimed = true;
        transform.Rotate(0f, 180f, 0f); 
    }

}

using UnityEngine;

public class NotificationZoneEffect : MonoBehaviour, IZoneEffect
{
    [SerializeField] private MonoBehaviour m_notificationService;
    private INotificationService NotificationService => m_notificationService as INotificationService;

    [SerializeField] string enterMessage;
    [SerializeField] string exitMessage;
    public void Apply()
    {
        NotificationService.Notify(enterMessage);
    }

    public void Unapply()
    {
        NotificationService.Notify(exitMessage);
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class NotificationController : MonoBehaviour, INotificationService
{
    [SerializeField] private NotificationWindow m_notificationWindow;
    [SerializeField] private float m_notificationDestroyTimer;

    public void Notify(string message)
    {
        m_notificationWindow.AddNotification(message, m_notificationDestroyTimer);
    }
}

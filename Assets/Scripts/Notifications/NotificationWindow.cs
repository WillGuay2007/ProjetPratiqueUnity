using TMPro;
using UnityEngine;

public class NotificationWindow : Window
{
    [SerializeField] private GameObject m_notificationCardPrefab;

    public void AddNotification(string message, float destroyTimer)
    {
        GameObject notifPrefab = Instantiate(m_notificationCardPrefab, transform);
        notifPrefab.GetComponentInChildren<TextMeshProUGUI>().text = message;
        notifPrefab.GetComponentInChildren<NotificationCard>().StartDestroyTimer(destroyTimer);
    }
}

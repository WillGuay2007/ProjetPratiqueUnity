using UnityEngine;

public class NotificationZoneEffect : MonoBehaviour, IZoneEffect
{
    [SerializeField] string enterMessage;
    [SerializeField] string exitMessage;
    public void Apply()
    {
        NotificationController.Instance.Notify(enterMessage);
    }

    public void Unapply()
    {
        NotificationController.Instance.Notify(exitMessage);
    }
}

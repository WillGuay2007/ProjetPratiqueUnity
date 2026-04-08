using Unity.VisualScripting;
using UnityEngine;

public class NotificationController : MonoBehaviour
{
    public static NotificationController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Notify(string message)
    {
        //TODO: Replace with actual UI notification system
        Debug.Log(message);
    }
}

using UnityEngine;

public class HUDController : MonoBehaviour
{
    private static HUDController _instance;
    
    public static HUDController Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("HUDController is null");
            return _instance;
        }
    }

    public void OpenWindow(Window window)
    {
        window.Show();
    }

    public void CloseWindow(Window window)
    {
        window.Hide();
    }

    private void Awake()
    {
        _instance = this;
    }
}

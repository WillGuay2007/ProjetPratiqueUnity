using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] public float cameraZOffset;
    [SerializeField] private PlayerCamera m_camera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<PlayerController>()) return;
        m_camera.currentZone = this;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.GetComponent<PlayerController>()) return;
        m_camera.currentZone = null;
    }
}

using UnityEngine;

public class CameraZoomEffect : MonoBehaviour, IZoneEffect
{
    [SerializeField] private float m_cameraZOffset;
    [SerializeField] private CameraController m_camera;

    public void Apply()
    {
        m_camera.SetCameraZoom(m_cameraZOffset);
    }

    public void Unapply()
    {
        m_camera.ResetCameraZoom();
    }
}

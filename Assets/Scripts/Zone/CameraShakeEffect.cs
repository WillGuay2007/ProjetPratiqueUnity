using UnityEngine;

public class CameraShakeEffect : MonoBehaviour, IZoneEffect
{
    [SerializeField] private float m_cameraShakeAmplitude;
    [SerializeField] private float m_shakeSpeed;
    [SerializeField] private PlayerCameraController m_camera;

    public void Apply()
    {
        m_camera.SetAmbientCameraShake(m_cameraShakeAmplitude, m_shakeSpeed);
    }

    public void Unapply()
    {
        m_camera.SetAmbientCameraShake(0f);
    }
}

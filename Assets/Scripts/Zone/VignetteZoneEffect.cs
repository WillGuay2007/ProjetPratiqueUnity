using UnityEngine;

public class VignetteZoneEffect : MonoBehaviour, IZoneEffect
{
    [SerializeField] private PostProcessController m_postProcessController;
    [SerializeField] private float m_vignetteIntensity;
    [SerializeField] private float m_vignetteFadeDuration;
    public void Apply(PlayerController controller)
    {
        m_postProcessController.SetVignette(m_vignetteIntensity, m_vignetteFadeDuration);
    }

    public void Unapply(PlayerController controller)
    {
        m_postProcessController.SetVignette(0f, m_vignetteFadeDuration);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessController : MonoBehaviour
{
    [SerializeField] private VolumeProfile m_profile;

    public void SetVignette(float intensity, float fadeDuration)
    {
        StartCoroutine(VignetteFade(intensity, fadeDuration));
    }

    private IEnumerator VignetteFade(float targetIntensity, float duration)
    {
        if (m_profile.TryGet<Vignette>(out Vignette m_vignette))
        {
            float startIntensity = m_vignette.intensity.value;
            float elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float newIntensity = Mathf.Lerp(startIntensity, targetIntensity, elapsed / duration);
                m_vignette.intensity.value = newIntensity;
                yield return null;
            }
            m_vignette.intensity.value = targetIntensity;
        }
    }
}

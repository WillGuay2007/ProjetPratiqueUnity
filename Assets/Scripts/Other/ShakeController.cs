using System.Collections;
using UnityEngine;

public class ShakeController : MonoBehaviour
{
    public static ShakeController Instance { get; private set; }
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

    public void SetAmbientShake(Transform transformToShake, float amplitude)
    {
        StartCoroutine(StartAmbientShakeCoroutine(transformToShake, amplitude));
    }

    public void SetImpactShake(Transform transformToShake, float amplitude, float duration)
    {
        StartCoroutine(StartImpactShakeCoroutine(transformToShake, amplitude, duration));
    }
    private IEnumerator StartAmbientShakeCoroutine(Transform transformToShake, float amplitude)
    {
        yield return null;
    }

    private IEnumerator StartImpactShakeCoroutine(Transform transformToShake, float amplitude, float duration)
    {
        while (duration > 0)
        {
            yield return new WaitForEndOfFrame();
            Vector3 shakeOffset = Random.insideUnitSphere * amplitude;
            transformToShake.localPosition += shakeOffset;
            duration -= Time.deltaTime;
        }
    }
}

using System.Collections;
using UnityEngine;
//TODO: Refactor
public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject m_target;
    [SerializeField] private float m_Speed;
    private bool m_isShaking = false;
    private Vector3 m_originalOffset = new Vector3(0, 0, -15);
    private Vector3 m_offset;

    private void Awake()
    {
        m_offset = m_originalOffset;
    }

    void Update()
    {
        MoveCamera();
    }

    public void ResetCameraZoom()
    {
        m_offset = m_originalOffset;
    }
    public void SetCameraZoom(float zoom)
    {
        m_offset = new Vector3(0, 0, -Mathf.Abs(zoom));
    }

    public void SetAmbientCameraShake(float shakeAmount, float shakeSpeed = 0f)
    {
        if (m_isShaking) m_isShaking = false;
        if (shakeAmount <= 0) return;
        m_isShaking = true;
        StartCoroutine(StartCameraShake(shakeAmount, shakeSpeed));
    }

    public void SetImpactShake(float intensity, float duration)
    {
        ShakeController.Instance.SetImpactShake(transform, intensity, duration);
    }

    private IEnumerator StartCameraShake(float shakeAmplitude, float shakeSpeed)
    {
        Vector3 originalPosition = transform.position;
        Vector3 shakeOffset = Vector3.zero;
        //Logique principale du shake.
        while (m_isShaking)
        {
            shakeOffset = Random.insideUnitSphere * shakeAmplitude;
            shakeOffset.z = 0;
            transform.position = transform.localPosition + shakeOffset;
            yield return null;
        }

        //Sinon ca snap. Alors je decrease progressivement le shake pour que ca soit plus smooth
        while (shakeOffset.magnitude > 0.01f)
        {
            shakeOffset = Vector3.Lerp(shakeOffset, Vector3.zero, Time.deltaTime * 5);
            transform.position = transform.localPosition + shakeOffset;
            yield return null;
        }

    }

    private void MoveCamera()
    {
        float dist = Vector3.Distance(transform.position, m_target.transform.position + m_offset);
        if (dist >= 0.25f)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                m_target.transform.position + m_offset,
                Time.deltaTime * dist * m_Speed);
        }
    }
}

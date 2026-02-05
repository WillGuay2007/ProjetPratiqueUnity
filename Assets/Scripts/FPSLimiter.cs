using UnityEngine;

public class FPSLimiter : MonoBehaviour
{
    [SerializeField] private int m_FPSCap;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = m_FPSCap;
    }
}

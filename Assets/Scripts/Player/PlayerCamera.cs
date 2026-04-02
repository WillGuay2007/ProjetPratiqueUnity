using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private GameObject m_Player;
    [SerializeField] private float m_Speed;
    private Vector3 m_originalOffset = new Vector3(0, 0, -15);
    public Zone currentZone;
    private Vector3 m_offset
    {
        get
        {
            if (currentZone != null)
            {
                return new Vector3(0, 0, -(Mathf.Abs(currentZone.cameraZOffset)));
            }
            else
            {
                return m_originalOffset;
            }
        }
    }

    void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        float dist = Vector3.Distance(transform.position, m_Player.transform.position + m_offset);
        if (dist >= 0.25f)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                m_Player.transform.position + m_offset,
                Time.deltaTime * dist * m_Speed);
        }
    }
}

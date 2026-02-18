using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private GameObject m_Player;
    [SerializeField] private float m_Speed;
    private Vector3 m_offset;
    void Awake()
    {
        m_offset = transform.position - m_Player.transform.position;
    }

    void Update()
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

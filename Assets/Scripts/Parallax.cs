using UnityEngine;
using UnityEngine.UIElements;

public class Parallax : MonoBehaviour
{
    public GameObject cam;
    [SerializeField] public float parallaxEffect; //Plus haut = moins rapide
    private float m_startPos, m_length;
    void Start()
    {
        m_startPos = transform.position.x;   
        m_length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void LateUpdate()
    {
        float distance = cam.transform.position.x * parallaxEffect; //La distance en x que le layer doit avoir
        float movement = cam.transform.position.x * (1 - parallaxEffect); //La distance x parcourue relatif a la camera

        transform.position = new Vector3(m_startPos + distance, transform.position.y, transform.position.z);

        if (movement > m_startPos + m_length)
        {
            m_startPos += m_length;
        } else if (movement < m_startPos - m_length)
        {
            m_startPos -= m_length;
        }
    }
}

using UnityEngine;
using UnityEngine.UIElements;

public class Parallax : MonoBehaviour
{
    //TODO: Make more readable with clean code principles
    public GameObject cam;
    [SerializeField] private float parallaxEffect; //Plus haut = moins rapide
    private float m_startPos, m_objectBoundsX;
    void Start()
    {
        m_startPos = transform.position.x;   
        m_objectBoundsX = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void LateUpdate()
    {
        float travelledDistanceRelativeToWorld = cam.transform.position.x * parallaxEffect; //La distance en x que le layer doit avoir
        float travelledDistanceRelativeToCamera = cam.transform.position.x * (1 - parallaxEffect); //La distance x parcourue relatif a la camera

        transform.position = new Vector3(m_startPos + travelledDistanceRelativeToWorld, transform.position.y, transform.position.z);

        if (travelledDistanceRelativeToCamera > m_startPos + m_objectBoundsX)
        {
            m_startPos += m_objectBoundsX;
        } else if (travelledDistanceRelativeToCamera < m_startPos - m_objectBoundsX)
        {
            m_startPos -= m_objectBoundsX;
        }
    }
}

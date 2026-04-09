using UnityEngine;

public class Window : MonoBehaviour
{
    [SerializeField] private int m_layer;

    private void Awake()
    {
        gameObject.GetComponentInParent<Canvas>().sortingOrder = m_layer;
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}

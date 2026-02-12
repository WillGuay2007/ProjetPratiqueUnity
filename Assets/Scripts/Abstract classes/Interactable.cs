using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private PlayerController m_player;
    private bool m_canInteract;
    private bool m_isInteractInputActive;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CollisionIsPlayer(collision) == false) return;
        m_canInteract = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (CollisionIsPlayer(collision) == false) return;
        m_canInteract = false;
    }

    private void Update()
    {
        if (m_player.isInteracting) return;
        m_isInteractInputActive = m_player.playerInputManager.InteractInput;
        if (m_isInteractInputActive && m_canInteract)
        {
            m_player.isInteracting = true;
            m_player.playerInputManager.UseInteractInput();
            Interact();
        }
    }

    private bool CollisionIsPlayer(Collider2D collision)
    {
        return collision.gameObject.GetComponent<PlayerController>() != null;
    }

    protected abstract void Interact();
    protected void DisableInteracting() => m_player.isInteracting = false;
}

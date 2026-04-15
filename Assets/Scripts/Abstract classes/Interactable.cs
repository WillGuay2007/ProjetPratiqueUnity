using System.Collections;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private PlayerController m_player;
    [SerializeField] private GameObject m_interactionBubblePrefab;
    private Animator m_interactionBubbleAnimator;
    private float m_interactionBubbleHeightOffset = 0.5f;
    private SpriteRenderer m_spriteRenderer;
    private bool m_canInteract;
    private bool m_isInteractInputActive;

    private void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_interactionBubbleAnimator = m_interactionBubblePrefab.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CollisionIsPlayer(collision) == false) return;
        SetInteractionBubble(true);
        m_canInteract = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (CollisionIsPlayer(collision) == false) return;
        SetInteractionBubble(false);
        m_canInteract = false;
    }

    private void Update()
    {
        if (m_player.isInteracting) return;
        m_isInteractInputActive = m_player.InputController.InteractInput;
        if (m_isInteractInputActive && m_canInteract)
        {
            m_player.InputController.UseInteractInput();
            m_player.isInteracting = true;
            StartCoroutine(Interact());
        }
    }

    private void SetInteractionBubble(bool enabled)
    {
        if (enabled)
        {
            m_interactionBubbleAnimator.SetBool("IsDisappeared", false);
            m_interactionBubblePrefab.transform.position = transform.position + (m_spriteRenderer.bounds.size.y / 2 + m_interactionBubbleHeightOffset) * Vector3.up;
        } else
        {
            m_interactionBubbleAnimator.SetBool("IsDisappeared", true);
        }
    }

    private bool CollisionIsPlayer(Collider2D collision)
    {
        return collision.gameObject.GetComponent<PlayerController>() != null;
    }

    protected abstract IEnumerator Interact();
    protected void DisableInteracting() => m_player.isInteracting = false;
}

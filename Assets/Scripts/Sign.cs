using UnityEngine;

public class Sign : Interactable
{
    [SerializeField] private DialogueManager m_dialogueManager;
    [SerializeField] private DialogueObject m_dialogueToSay;
    protected override void Interact()
    {
        m_dialogueManager.ShowDialogue(m_dialogueToSay);
        DisableInteracting();
    }
}

using System.Collections;
using UnityEngine;

public class Sign : Interactable
{
    [SerializeField] private DialogueController m_dialogueManager;
    [SerializeField] private DialogueObject m_dialogueToSay;
    protected override IEnumerator Interact()
    {
        yield return m_dialogueManager.ShowDialogue(m_dialogueToSay);
        yield return new WaitForSeconds(1);
        DisableInteracting();
    }
}

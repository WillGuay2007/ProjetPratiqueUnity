using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private PlayerController m_playerController;
    [SerializeField] DialogueWindow m_dialogueWindow;
    [SerializeField] private MonoBehaviour dialogueStrategy; //TEMPORARY, should be migrated to scriptable objects when I can.
    private IDialogueStrategy DialogueStrategy => dialogueStrategy as IDialogueStrategy;



    private void Start()
    {
        EndDialogue();
    }

    public Coroutine ShowDialogue(DialogueObject dialogueObject)
    {
        m_playerController.SetPhysics(false);
        m_playerController.InputController.SetInputEnabled(false);
        HUDController.Instance.OpenWindow(m_dialogueWindow);
        return StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        foreach(string dialogue in dialogueObject.Dialogue)
        {
            yield return m_dialogueWindow.SetText(dialogue, DialogueStrategy);
        }

        EndDialogue();
    }

    private void EndDialogue()
    {
        m_playerController.InputController.SetInputEnabled(true);
        m_playerController.SetPhysics(true);
        HUDController.Instance.CloseWindow(m_dialogueWindow);
        m_dialogueWindow.ResetWindow();
    }
}

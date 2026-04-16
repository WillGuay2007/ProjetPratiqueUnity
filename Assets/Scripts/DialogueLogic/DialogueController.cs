using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private PlayerController m_playerController;
    [SerializeField] DialogueWindow m_dialogueWindow;
    [SerializeField] private MonoBehaviour m_defaultDialogueStrategy;

    #region Dialogue strategies
    [Header("Dialogue strategies")]
    [SerializeField] private MonoBehaviour m_normalStrategy;
    [SerializeField] private MonoBehaviour m_fearStrategy;
    #endregion
    private IDialogueStrategy DefaultDialogueStrategy => m_defaultDialogueStrategy as IDialogueStrategy;


    private IDialogueStrategy GetStrategy(DialogueStrategyType type)
    {
        return type switch
        {
            DialogueStrategyType.Fear => m_fearStrategy as IDialogueStrategy,
            DialogueStrategyType.Normal => m_normalStrategy as IDialogueStrategy,
            _ => DefaultDialogueStrategy
        };
    }



    private void Start()
    {
        EndDialogue();
    }

    public Coroutine ShowDialogue(DialogueObject dialogueObject)
    {
        if (DefaultDialogueStrategy == null)
        {
            Debug.LogError("Default dialogue strategy is null.");
            return null;
        }
        m_playerController.GravityController.SetPhysics(false);
        m_playerController.InputController.SetInputEnabled(false);
        HUDController.Instance.OpenWindow(m_dialogueWindow);
        return StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        foreach (DialogueLine line in dialogueObject.DialogueLines)
        {
            IDialogueStrategy strategy = GetStrategy(line.strategyType);
            yield return m_dialogueWindow.SetText(line.text, strategy);
        }
        EndDialogue();
    }

    private void EndDialogue()
    {
        m_playerController.InputController.SetInputEnabled(true);
        m_playerController.GravityController.SetPhysics(true);
        HUDController.Instance.CloseWindow(m_dialogueWindow);
        m_dialogueWindow.ResetWindow();
    }
}

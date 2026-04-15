using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TMP_Text m_textLabel;
    [SerializeField] private GameObject m_dialogueBox;
    [SerializeField] private PlayerController m_playerController;

    private TypeWriterEffect m_typeWriterEffect;

    private void Start()
    {
        m_typeWriterEffect = GetComponent<TypeWriterEffect>();
        CloseDialogueBox();
    }

    public Coroutine ShowDialogue(DialogueObject dialogueObject)
    {
        m_playerController.SetPhysics(false);
        m_playerController.InputController.SetInputEnabled(false);
        m_dialogueBox.SetActive(true);
        return StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        foreach(string dialogue in dialogueObject.Dialogue)
        {
            yield return null;
            yield return m_typeWriterEffect.Run(dialogue, m_textLabel);
            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        } //TODO: Rendre ceci plus lisable. les 2 yield return null fixent le double input de GetKeyDown();

        CloseDialogueBox();
    }

    private void CloseDialogueBox()
    {
        m_playerController.InputController.SetInputEnabled(true);
        m_playerController.SetPhysics(true);
        m_dialogueBox.SetActive(false);
        m_textLabel.text = string.Empty;
    }
}

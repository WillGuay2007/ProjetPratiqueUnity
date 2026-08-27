using UnityEngine;


[System.Serializable]
public class DialogueLine
{
    [TextArea] public string text;
    [SerializeReference, SerializeReferenceDropdown]
    public IDialogueDisplayStrategy displayStrategy;
}

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] private DialogueLine[] m_dialogueLines;
    public DialogueLine[] DialogueLines => m_dialogueLines;
}
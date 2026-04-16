using UnityEngine;

public enum DialogueStrategyType { Default, Normal, Fear }

[System.Serializable]
public class DialogueLine
{
    [TextArea] public string text;
    public DialogueStrategyType strategyType;
}

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] private DialogueLine[] m_dialogueLines;
    public DialogueLine[] DialogueLines => m_dialogueLines;
}
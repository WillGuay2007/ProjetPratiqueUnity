using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField][TextArea] private string[] m_dialogue;
    /*[SerializeField] private MonoBehaviour m_dialogueStrategy;*/ //TODO: Add a way to make this work

    public string[] Dialogue => m_dialogue;
    //public IDialogueStrategy DialogueStrategy => m_dialogueStrategy as IDialogueStrategy;
}

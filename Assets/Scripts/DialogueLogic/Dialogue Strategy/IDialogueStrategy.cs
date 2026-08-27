using UnityEngine;

public interface IDialogueStrategy
{
    public void ShowDialogue(DialogueObject dialogueObj);
    public void EndDialogue();
}

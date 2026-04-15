using System.Collections;
using UnityEngine;

public interface IDialogueStrategy
{
    public IEnumerator DisplayDialogue(DialogueWindow window, string textToDisplay);
}

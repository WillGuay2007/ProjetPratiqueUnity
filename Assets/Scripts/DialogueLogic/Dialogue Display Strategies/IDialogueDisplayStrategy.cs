using System.Collections;
using UnityEngine;

public interface IDialogueDisplayStrategy
{
    public IEnumerator DisplayDialogue(DialogueWindow window, string textToDisplay);
}

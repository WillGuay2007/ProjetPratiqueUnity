using System.Collections;
using UnityEngine;

public class NormalDialogueStrategy : MonoBehaviour, IDialogueStrategy
{
    private const float SHAKE_DURATION = 0.25f;
    private const float SHAKE_AMPLITUDE = 5f;
    [SerializeField] private TypeWriterEffect m_typeWriterEffect;

    public IEnumerator DisplayDialogue(DialogueWindow window, string textToDisplay)
    {
        yield return null;
        ShakeController.Instance.SetImpactShake(window.transform, SHAKE_AMPLITUDE, SHAKE_DURATION);
        yield return m_typeWriterEffect.Run(textToDisplay, window.TextLabel);
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
    }
}

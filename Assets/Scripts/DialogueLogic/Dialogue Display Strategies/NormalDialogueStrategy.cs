using System.Collections;
using UnityEngine;

public class NormalDialogueStrategy : MonoBehaviour, IDialogueDisplayStrategy
{
    private const float SHAKE_DURATION = 0.25f;
    private const float SHAKE_AMPLITUDE = 5f;
    private TypeWriterEffect m_typeWriterEffect;

    private void Awake() => m_typeWriterEffect = GetComponent<TypeWriterEffect>();

    public IEnumerator DisplayDialogue(DialogueWindow window, string textToDisplay)
    {
        yield return null;
        ShakeController.Instance.SetImpactShake(window.transform, SHAKE_AMPLITUDE, SHAKE_DURATION);

        bool isTypingDone = false;
        m_typeWriterEffect.Run(textToDisplay, window.TextLabel, () => isTypingDone = true);

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E) || isTypingDone);

        if (!isTypingDone) m_typeWriterEffect.Cancel();

        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
    }
}

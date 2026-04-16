using System.Collections;
using UnityEngine;

public class FearDialogueStrategy : MonoBehaviour, IDialogueStrategy
{
    private TypeWriterEffect m_typeWriterEffect;
    private TextShakeEffect m_textShakeEffect;
    private void Awake()
    {
        m_textShakeEffect = GetComponent<TextShakeEffect>();
        m_typeWriterEffect = GetComponent<TypeWriterEffect>();
    }
    public IEnumerator DisplayDialogue(DialogueWindow window, string textToDisplay)
    {
        bool isFinished = false;
        m_typeWriterEffect.Run(textToDisplay, window.TextLabel, () => isFinished = true);
        yield return null;
        m_textShakeEffect.Run(window.TextLabel, 2f);

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E) || isFinished == true);
    
        if (!isFinished) m_typeWriterEffect.Cancel();

        yield return null;

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        m_textShakeEffect.Stop();
    }
}

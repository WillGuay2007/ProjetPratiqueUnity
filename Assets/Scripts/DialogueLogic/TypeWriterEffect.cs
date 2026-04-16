using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TypeWriterEffect : MonoBehaviour
{

    [SerializeField] private float m_charactersPerSecond = 50f;
    private bool m_isCancelled = false;

    public Coroutine Run(string textToType, TMP_Text textLabel, Action onComplete = null)
    {
        return StartCoroutine(TypeText(textToType, textLabel, onComplete));
    }

    public void Cancel()
    {
        m_isCancelled = true;
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel, Action onComplete)
    {
        m_isCancelled = false;
        textLabel.text = string.Empty;
        float t = 0;
        int charIndex = 0;
        while (charIndex < textToType.Length)
        {
            if (m_isCancelled) break;
            t += Time.deltaTime * m_charactersPerSecond;
            charIndex = Mathf.Clamp(Mathf.FloorToInt(t), 0, textToType.Length);
            textLabel.text = textToType.Substring(0, charIndex);
            yield return null;
        }
        textLabel.text = textToType;
        onComplete?.Invoke();
    }
}

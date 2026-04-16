using System.Collections;
using TMPro;
using UnityEngine;

public class TextShakeEffect : MonoBehaviour
{
    private Coroutine m_activeCoroutine = null;

    public Coroutine Run(TMP_Text text, float shakeIntensity)
    {
        return m_activeCoroutine = StartCoroutine(ShakeVertices(text, shakeIntensity));
    }

    public void Stop()
    {
        StopCoroutine(m_activeCoroutine);
    }

    private IEnumerator ShakeVertices(TMP_Text textLabel, float shakeIntensity)
    {
        textLabel.ForceMeshUpdate();
        Vector3[] originalVertices = textLabel.textInfo.meshInfo[0].vertices.Clone() as Vector3[];
        int lastCharacterCount = textLabel.textInfo.characterCount;

        while (true)
        {
            textLabel.ForceMeshUpdate();

            if (textLabel.textInfo.characterCount != lastCharacterCount)
            {
                originalVertices = textLabel.textInfo.meshInfo[0].vertices.Clone() as Vector3[];
                lastCharacterCount = textLabel.textInfo.characterCount;
            }

            Vector3[] vertices = textLabel.textInfo.meshInfo[0].vertices;

            for (int i = 0; i < textLabel.textInfo.characterCount; i++)
            {
                TMP_CharacterInfo charInfo = textLabel.textInfo.characterInfo[i];
                if (!charInfo.isVisible) continue;

                Vector3 shake = new Vector3(Random.Range(-shakeIntensity, shakeIntensity), Random.Range(-shakeIntensity, shakeIntensity), 0);
                vertices[charInfo.vertexIndex + 0] = originalVertices[charInfo.vertexIndex + 0] + shake;
                vertices[charInfo.vertexIndex + 1] = originalVertices[charInfo.vertexIndex + 1] + shake;
                vertices[charInfo.vertexIndex + 2] = originalVertices[charInfo.vertexIndex + 2] + shake;
                vertices[charInfo.vertexIndex + 3] = originalVertices[charInfo.vertexIndex + 3] + shake;
            }

            textLabel.textInfo.meshInfo[0].mesh.vertices = textLabel.textInfo.meshInfo[0].vertices;
            textLabel.UpdateGeometry(textLabel.textInfo.meshInfo[0].mesh, 0);

            yield return null;
        }
    }
}
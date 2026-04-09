using System.Collections;
using UnityEngine;

public class NotificationCard : MonoBehaviour
{
    public void StartDestroyTimer(float timer)
    {
        StartCoroutine(DestroyTimer(timer));
    }

    private IEnumerator DestroyTimer(float timer)
    {
        float elapsedTime = 0f;
        CanvasRenderer renderer = GetComponent<CanvasRenderer>();
        while (elapsedTime < timer)
        {
            elapsedTime += Time.deltaTime;
            renderer.SetAlpha( 1 - (elapsedTime / timer) );
            yield return null;
        }
        Destroy(gameObject);
    }
}

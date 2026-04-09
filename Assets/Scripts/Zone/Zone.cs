using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    private List<IZoneEffect> effects = new List<IZoneEffect>();

    private void Awake()
    {
        foreach (IZoneEffect effect in GetComponents<IZoneEffect>())
        {
            effects.Add(effect);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<PlayerController>()) return;
        foreach (IZoneEffect effect in effects)
        {
            effect.Apply();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.GetComponent<PlayerController>()) return;
        foreach (IZoneEffect effect in effects)
        {
            effect.Unapply();
        }
    }
}

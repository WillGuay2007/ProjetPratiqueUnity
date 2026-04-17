using System;
using UnityEngine;
public class PlayerDeathController : MonoBehaviour
{
    private Action m_onDeath;

    public void Initialize(Action onDeath) => m_onDeath = onDeath;

    public void Die()
    {
        m_onDeath?.Invoke();
    }
}
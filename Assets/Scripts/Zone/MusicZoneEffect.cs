using UnityEngine;

public class MusicZoneEffect : MonoBehaviour, IZoneEffect
{
    [SerializeField] private AudioClip m_musicClip;
    public void Apply()
    {
        AudioController.Instance.PlayNewMusicWithFade(m_musicClip);
    }

    public void Unapply()
    {
        AudioController.Instance.PlayDefaultMusic();
    }
}

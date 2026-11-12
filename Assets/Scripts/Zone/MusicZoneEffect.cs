using UnityEngine;

public class MusicZoneEffect : MonoBehaviour, IZoneEffect
{
    [SerializeField] private AudioClip m_musicClip;
    public void Apply(PlayerController controller)
    {
        AudioController.Instance.PlayNewMusicWithFade(m_musicClip);
    }

    public void Unapply(PlayerController controller)
    {
        AudioController.Instance.PlayDefaultMusic();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public enum Sounds
    {
        PlayerJump,
    }

    [System.Serializable]
    public struct SoundEntry
    {
        public Sounds sound;
        public AudioClip clip;
    }

    [SerializeField] private float m_fadeDuration;
    [SerializeField] AudioSource m_musicSource1;
    [SerializeField] AudioSource m_musicSource2;
    [SerializeField] AudioSource m_soundSource;
    [SerializeField] private SoundEntry[] m_soundEntries;
    [SerializeField] private AudioClip m_defaultMusic;

    private float m_musicVolume = 1f;

    private Dictionary<Sounds, AudioClip> m_sounds = new();

    private bool m_isMusicSource1Active = true;

    private Coroutine m_crossFadeCoroutine;

    private static AudioController m_instance;
    public static AudioController Instance {
        get
        {
            //if (m_instance == null) print("AudioController does not exist.");
            return m_instance; 
        } 
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        m_instance = this;
        DontDestroyOnLoad(m_instance);

        foreach (var entry in m_soundEntries)
        {
            m_sounds.Add(entry.sound, entry.clip);
        }
        m_musicSource1.loop = true;
        m_musicSource2.loop = true;
        SetMusicVolume(m_musicVolume);
        PlayDefaultMusic();
    }

    public void PlaySound(Sounds sound)
    {
        if (m_sounds.ContainsKey(sound))
        {
            m_soundSource.PlayOneShot(m_sounds[sound]);
        }
    }

    public void SetMusicVolume(float volume)
    {
        if (m_crossFadeCoroutine != null)
        {
            m_musicVolume = volume;
            return;
        }
        m_musicSource1.volume = volume;
        m_musicSource2.volume = volume;
        m_musicVolume = volume;
    }

    public void SetSoundVolume(float volume)
    {
        m_soundSource.volume = volume;
    }

    public void PlayDefaultMusic()
    {
        PlayNewMusicWithFade(m_defaultMusic);
    }

    public void PlayNewMusicWithFade(AudioClip music)
    {
        if (m_crossFadeCoroutine != null) StopCoroutine(m_crossFadeCoroutine);
        m_crossFadeCoroutine = StartCoroutine(MusicCrossFade(music));
    }

    private IEnumerator MusicCrossFade(AudioClip music)
    {
        float elapsedTime = 0f;
        if (m_isMusicSource1Active)
        {
            m_musicSource2.clip = music;
            m_musicSource2.Play();
            while (elapsedTime < m_fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float lerpFraction = elapsedTime / m_fadeDuration;
                m_musicSource1.volume = Mathf.Lerp(m_musicVolume, 0f, lerpFraction);
                m_musicSource2.volume = Mathf.Lerp(0f, m_musicVolume, lerpFraction);
                yield return null;
            }
            m_musicSource1.volume = 0f;
            m_isMusicSource1Active = false;
        } 
        else
        {
            m_musicSource1.clip = music;
            m_musicSource1.Play();
            while (elapsedTime < m_fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float lerpFraction = elapsedTime / m_fadeDuration;
                m_musicSource2.volume = Mathf.Lerp(m_musicVolume, 0f, lerpFraction);
                m_musicSource1.volume = Mathf.Lerp(0f, m_musicVolume, lerpFraction);
                yield return null;
            }
            m_musicSource2.volume = 0f;
            m_isMusicSource1Active = true;
        }
    }
}

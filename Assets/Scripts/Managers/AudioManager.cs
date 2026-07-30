using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    private readonly float musicVolume = 0.3f;
    private readonly float sfxVolume = 0.8f;

    private void Awake()
    {
        Instance = this;

        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;
    }

    public void SetMusicVolume(float value)
    {
        musicSource.volume = value;
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    public void SetSFXVolume(float value)
    {
        sfxSource.volume = value;
    }
}

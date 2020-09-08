using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { private set; get; }

    [Header("References")]
    public AudioSource musicSource = null;
    public AudioSource sfxMenuSource = null;
    public AudioSource[] sfxInGameSources = null;

    [Header("Clips")]
    public AudioClip menuNavigationSfx = null;
    public AudioClip menuConfirmationSfx = null;
    public AudioClip menuConfirmationFailedSfx = null;
    public AudioClip menuBackSfx = null;

    private int _currentInGameIndex = 0;

    private void Awake()
    {
        // First destroy any existing instance of it
        if (instance != null)
        {
            Destroy(instance);
        }

        // Then reassign a proper one
        instance = this;

        DontDestroyOnLoad(this);
    }

    public void PlayMusic(AudioClip music)
    {
        if (musicSource.clip == null || musicSource.clip.name != music.name)
        {
            musicSource.clip = music;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlayMenuSound(AudioClip sfx)
    {
        sfxMenuSource.clip = sfx;
        sfxMenuSource.Play();
    }

    public void PlayInGameSound(AudioClip sfx)
    {
        sfxInGameSources[_currentInGameIndex].clip = sfx;
        sfxInGameSources[_currentInGameIndex].Play();

        if (++_currentInGameIndex >= sfxInGameSources.Length)
        {
            _currentInGameIndex = 0;
        }
    }
}

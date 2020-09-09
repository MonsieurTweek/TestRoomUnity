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
    private float _musicVolume = 0f;
    private float _sfxVolume = 0f;

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

        _musicVolume = musicSource.volume;
        _sfxVolume = sfxMenuSource.volume;
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

    public void FadeInMusic()
    {
        LeanTween.value(0f, _musicVolume, 1f).setOnUpdate(OnMusicVolumeUpdated);
    }

    public void FadeOutMusic(float volume = 0f)
    {
        LeanTween.value(musicSource.volume, volume, 1f).setOnUpdate(OnMusicVolumeUpdated);
    }

    private void OnMusicVolumeUpdated(float volume)
    {
        musicSource.volume = volume;
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

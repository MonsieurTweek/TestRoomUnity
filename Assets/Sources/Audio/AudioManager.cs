using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { private set; get; }

    public AudioSource musicSource = null;
    public AudioSource sfxSource = null;

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
}

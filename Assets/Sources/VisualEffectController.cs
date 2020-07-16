using UnityEngine;

public class VisualEffectController : MonoBehaviour
{
    public bool isPersistent = true;
    public ParticleSystem effect = null;

    public uint stateId { private set; get; }

    public void Reset(uint stateId)
    {
        this.stateId = stateId;

        Play();
    }

    private void Start()
    {
        LoadingGameEvent.instance.onLoadingEnded += OnLoadingEnded;
    }

    /// <summary>
    /// Disable effect on loading new menu state
    /// </summary>
    private void OnLoadingEnded()
    {
        Stop();
    }

    public void Play()
    {
        gameObject.SetActive(true);

        effect.Play();
    }

    public void Stop()
    {
        effect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (LoadingGameEvent.instance != null)
        {
            LoadingGameEvent.instance.onLoadingEnded -= OnLoadingEnded;
        }
    }
}
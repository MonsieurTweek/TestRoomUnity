using UnityEngine;

public class VisualEffectController : MonoBehaviour
{
    public ParticleSystem effect = null;

    private void Start()
    {
        LoadingGameEvent.instance.onLoadingEnded += OnLoadingEnded;
    }

    /// <summary>
    /// Disable effect on loading new state
    /// </summary>
    private void OnLoadingEnded()
    {
        effect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    private void OnDestroy()
    {
        if (LoadingGameEvent.instance != null)
        {
            LoadingGameEvent.instance.onLoadingEnded -= OnLoadingEnded;
        }
    }
}
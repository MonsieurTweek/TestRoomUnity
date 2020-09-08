using UnityEngine;

public class VisualEffectController : MonoBehaviour
{
    public bool isPersistent = true;
    public ParticleSystem effect = null;
    public AudioClip sound = null;

    public uint stateId { private set; get; }

    private Transform _parent = null;
    private bool _hasBeenAttached = false;

    public void Reset(uint stateId)
    {
        this.stateId = stateId;

        Play();
    }

    private void Start()
    {
        LoadingGameEvent.instance.onLoadingEnded += OnLoadingEnded;

        // Save parent to reassign it later
        _parent = transform.parent;
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

        if (sound != null)
        {
            AudioManager.instance.PlayInGameSound(sound);
        }

        effect.Play();
    }

    public void Stop()
    {
        effect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        gameObject.SetActive(false);

        if (_hasBeenAttached == true)
        {
            transform.SetParent(_parent);
        }
    }

    public void AttachToAnchor(Transform anchor)
    {
        _hasBeenAttached = true;

        transform.position = anchor.position;
        transform.rotation = anchor.rotation;

        transform.SetParent(anchor);
    }

    private void OnDestroy()
    {
        if (LoadingGameEvent.instance != null)
        {
            LoadingGameEvent.instance.onLoadingEnded -= OnLoadingEnded;
        }
    }
}
using System;
using UnityEngine;

public class LoadingGameEvent : MonoBehaviour
{
    public static LoadingGameEvent instance { private set; get; }

    private void Awake()
    {
        // First destroy any existing instance of it
        if (instance != null)
        {
            Destroy(instance);
        }

        // Then reassign a proper one
        instance = this;
    }

    public event Action<Action> onPrepare;
    public void Prepare(Action callback)
    {
        if (instance.onPrepare != null)
        {
            instance.onPrepare(callback);
        }
    }

    public event Action<int> onLoadingStarted;
    public void LoadingStarted(int objectCountToLoad)
    {
        if (instance.onLoadingStarted != null)
        {
            instance.onLoadingStarted(objectCountToLoad);
        }
    }

    public event Action<float> onLoadingProgress;
    public void LoadingProgress(float progress)
    {
        if (instance.onLoadingProgress != null)
        {
            instance.onLoadingProgress(progress);
        }
    }

    public event Action onLoadingEnded;
    public void LoadingEnded()
    {
        if (instance.onLoadingEnded != null)
        {
            instance.onLoadingEnded();
        }
    }
}
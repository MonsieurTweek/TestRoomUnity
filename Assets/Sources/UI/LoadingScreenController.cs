using System;
using System.Collections;
using UnityEngine;

public class LoadingScreenController : MonoBehaviour
{
    [Header("References")]
    public GameObject root = null;
    public ProgressBarController loadingBar = null;

    [Header("Properties")]
    public float loadingDelay = 0.3f;
    public float loadingMinDuration = 1f;

    private Action _onLoadingPrepared = null;

    private int _loadingObjectCount = 0;
    private float _loadingProgress = 0;
    private float _startTime = 0f;

    private bool _isLoading = false;

    private void Awake()
    {
        root.SetActive(false);

        LoadingGameEvent.instance.onPrepare += OnPrepareLoading;
        LoadingGameEvent.instance.onLoadingStarted += OnLoadingStarted;
        LoadingGameEvent.instance.onLoadingProgress += OnLoadingProgress;
        LoadingGameEvent.instance.onLoadingEnded += OnLoadingEnded;
    }

    /// <summary>
    /// Prepare the loading display (eg. show the curtain before loading starts)
    /// </summary>
    private void OnPrepareLoading(Action callback)
    {
        _onLoadingPrepared = callback;

        // Reset loadingBar fill
        loadingBar.current = 0;

        // Start the loading (for fake display)
        _startTime = Time.time;

        // Force progress to zero
        _loadingObjectCount = 1;
        _loadingProgress = 0f;
        _isLoading = true;

        root.SetActive(true);

        StartCoroutine(PrepareLoading());
    }

    /// <summary>
    /// Prepare loading screen before changing state
    /// </summary>
    private IEnumerator PrepareLoading()
    {
        yield return new WaitForSeconds(loadingDelay);

        if (_onLoadingPrepared != null)
        {
            _onLoadingPrepared();
        }
    }

    /// <summary>
    /// Start loading monitoring and progression display
    /// </summary>
    /// <param name="objectCountToLoad">The amount of object to load</param>
    private void OnLoadingStarted(int objectCountToLoad)
    {
        _loadingObjectCount = objectCountToLoad;

        StartCoroutine(MonitorLoadingTime());
    }

    /// <summary>
    /// Keep track of loading progress
    /// </summary>
    /// <param name="progress">The current progress</param>
    private void OnLoadingProgress(float progress)
    {
        _loadingProgress = progress;
    }

    /// <summary>
    /// Disable loading on loading ended
    /// </summary>
    private void OnLoadingEnded()
    {
        root.SetActive(false);

        _isLoading = false;
    }

    /// <summary>
    /// Keep track of loading time to prevent blink
    /// </summary>
    private IEnumerator MonitorLoadingTime()
    {
        // Always wait for a delay to avoid blink on loading screen
        while (Time.time - _startTime < loadingMinDuration)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        LoadingGameEvent.instance.LoadingEnded();
    }

    private void Update()
    {
        if (_isLoading == true)
        {
            loadingBar.current = Mathf.RoundToInt(((Time.time - _startTime) / loadingMinDuration) * 70f) // 70% of the bar is fake loading to show a progress
                + Mathf.RoundToInt((_loadingProgress / _loadingObjectCount) * 30f); // 30% of the bar is real loading to finish progress
        }
    }

    private void OnDestroy()
    {
        if (GameEvent.instance != null)
        {
            LoadingGameEvent.instance.onPrepare -= OnPrepareLoading;
            LoadingGameEvent.instance.onLoadingStarted -= OnLoadingStarted;
            LoadingGameEvent.instance.onLoadingProgress -= OnLoadingProgress;
            LoadingGameEvent.instance.onLoadingEnded -= OnLoadingEnded;
        }
    }

}
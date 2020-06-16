using System.Collections;
using UnityEngine;

public class LoadingScreenController : MonoBehaviour
{
    [Header("References")]
    public GameObject root = null;
    public ProgressBarController loadingBar = null;

    [Header("Properties")]
    public float loadingDelay = 0.5f;

    private bool _isLoading = false;
    private float _startTime = 0f;
    private int _loadingObjectCount = 0;
    private float _loadingProgress = 0;

    private void Awake()
    {
        root.SetActive(false);
    }

    private void Start()
    {
        LoadingGameEvent.instance.onLoadingPrepared += OnLoadingPrepared;
        LoadingGameEvent.instance.onLoadingStarted += OnLoadingStarted;
        LoadingGameEvent.instance.onLoadingProgress += OnLoadingProgress;
        LoadingGameEvent.instance.onLoadingEnded += OnLoadingEnded;
    }

    /// <summary>
    /// Prepare the loading display (eg. show the curtain before loading starts)
    /// </summary>
    private void OnLoadingPrepared()
    {
        // Force progress to zero
        _loadingObjectCount = 1;
        _loadingProgress = 0f;

        loadingBar.current = 0;

        root.SetActive(true);
    }

    /// <summary>
    /// Start loading monitoring and progression display
    /// </summary>
    /// <param name="objectCountToLoad">The amount of object to load</param>
    private void OnLoadingStarted(int objectCountToLoad)
    {
        _isLoading = true;
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
        _startTime = Time.time;

        // Always wait for a delay to avoid blink on loading screen
        while (Time.time - _startTime < loadingDelay)
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
            loadingBar.current = Mathf.RoundToInt(((Time.time - _startTime) / loadingDelay) * 70f) // 70% of the bar is fake loading to show a progress
                + Mathf.RoundToInt((_loadingProgress / _loadingObjectCount) * 30f); // 30% of the bar is real loading to finish progress
        }
    }

    private void OnDestroy()
    {
        if (GameEvent.instance != null)
        {
            LoadingGameEvent.instance.onLoadingPrepared -= OnLoadingPrepared;
            LoadingGameEvent.instance.onLoadingStarted -= OnLoadingStarted;
            LoadingGameEvent.instance.onLoadingProgress -= OnLoadingProgress;
            LoadingGameEvent.instance.onLoadingEnded -= OnLoadingEnded;
        }
    }

}
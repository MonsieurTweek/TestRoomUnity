using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFSM : AbstractFSM
{
    [Header("States")]
    public GameStateWithScene stateHome = new GameStateWithScene();
    public GameStateWithScene stateStore = new GameStateWithScene();

    public GameStateWithScene stateCharacterSelection = new GameStateWithScene();
    public GameStateWithScene stateArena = new GameStateWithScene();
    public GameStateGameOver stateGameOver = new GameStateGameOver();

    // Transitions to states
    public void TransitionToHome() { LoadState(stateHome); }
    public void TransitionToStore() { LoadState(stateStore); }
    public void TransitionToCharacterSelection() { LoadState(stateCharacterSelection); }
    public void TransitionToArena() { LoadState(stateArena); }
    public void TransitionToGameOver(bool hasWon) { ChangeState(stateGameOver, hasWon); }

    private List<AsyncOperation> _scenesLoading = new List<AsyncOperation>();
    private float _totalSceneProgress = 0f;

    private void Start()
    {
        GameEvent.instance.onHomeButtonPressed += TransitionToHome;
        GameEvent.instance.onStoreButtonPressed += TransitionToStore;
        GameEvent.instance.onPlayButtonPressed += TransitionToCharacterSelection;
        GameEvent.instance.onCharacterSelected += TransitionToArena;
        GameEvent.instance.onGameOver += TransitionToGameOver;

        TransitionToHome();
    }

    /// <summary>
    /// Change to a new state with a loading screen as transition
    /// </summary>
    /// <param name="newState">The new state to change to</param>
    private void LoadState(State newState)
    {
        StartCoroutine(PrepareLoading(newState));
    }

    /// <summary>
    /// Prepare loading screen before changing state
    /// </summary>
    /// <param name="newState">The new state to change to</param>
    /// <returns></returns>
    private IEnumerator PrepareLoading(State newState)
    {
        // Clear scenes already loaded
        _scenesLoading.Clear();

        LoadingGameEvent.instance.LoadingPrepared();

        yield return new WaitForSeconds(0.1f);

        ChangeState(newState);
    }

    /// <summary>
    /// Register an async operation to process (eg. loading a scene)
    /// </summary>
    /// <param name="operation">The operation to register</param>
    public void RegisterLoadingOperation(AsyncOperation operation)
    {
        _scenesLoading.Add(operation);
    }

    /// <summary>
    /// Start the loading for real
    /// </summary>
    public void StartLoading()
    {
        LoadingGameEvent.instance.LoadingStarted(_scenesLoading.Count);

        StartCoroutine(MonitorLoadingSceneProgress());
    }

    /// <summary>
    /// Keep track of current loading operations
    /// </summary>
    private IEnumerator MonitorLoadingSceneProgress()
    {
        for (int i = 0; i < _scenesLoading.Count; i++)
        {
            while (_scenesLoading[i].isDone == false)
            {
                _totalSceneProgress = 0f;

                foreach (AsyncOperation operation in _scenesLoading)
                {
                    _totalSceneProgress += operation.progress;
                }

                LoadingGameEvent.instance.LoadingProgress(_totalSceneProgress);

                yield return null;
            }
        }
    }

    private void OnDestroy()
    {
        if (GameEvent.instance != null)
        {
            GameEvent.instance.onHomeButtonPressed -= TransitionToHome;
            GameEvent.instance.onStoreButtonPressed -= TransitionToStore;
            GameEvent.instance.onPlayButtonPressed -= TransitionToCharacterSelection;
            GameEvent.instance.onCharacterSelected -= TransitionToArena;
            GameEvent.instance.onGameOver -= TransitionToGameOver;
        }
    }
}
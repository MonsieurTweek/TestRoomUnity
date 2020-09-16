using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFSM : AbstractFSM
{
    [Header("States")]
    public GameStateWithScene stateTitle = new GameStateWithScene();
    public GameStateWithScene stateHome = new GameStateWithScene();
    public GameStateWithScene stateStore = new GameStateWithScene();

    public GameStateWithScene stateCharacterSelection = new GameStateWithScene();
    public GameStateWithScene stateArena = new GameStateWithScene();

    [Header("Properties")]
    public float fadeOutTime = 0.5f;
    public bool loadTitleScene = true;

    // Transitions to states
    public void TransitionToTitle() { ChangeState(stateTitle); }
    public void TransitionToHome() { PrepareToLoadState(stateHome); }
    public void TransitionToStore() { PrepareToLoadState(stateStore); }
    public void TransitionToCharacterSelection() { PrepareToLoadState(stateCharacterSelection); }
    public void TransitionToArena() { PrepareToLoadState(stateArena); }

    private List<AsyncOperation> _scenesLoading = new List<AsyncOperation>();
    private float _totalSceneProgress = 0f;
    private State _stateToLoad = null;

    private void Start()
    {
        stateTitle.flag = (uint)GameStateEnum.TITLE;
        stateHome.flag = (uint)GameStateEnum.HOME;
        stateStore.flag = (uint)GameStateEnum.STORE;
        stateCharacterSelection.flag = (uint)GameStateEnum.SELECTION;
        stateArena.flag = (uint)GameStateEnum.ARENA;

        GameEvent.instance.onHomeButtonPressed += TransitionToHome;
        GameEvent.instance.onStoreButtonPressed += TransitionToStore;
        GameEvent.instance.onPlayButtonPressed += TransitionToCharacterSelection;
        GameEvent.instance.onResetButtonPressed += ResetProgression;
        GameEvent.instance.onCharacterSelected += TransitionToArena;
        GameEvent.instance.onGameOver += PrepareGameOver;

        if(loadTitleScene == true)
        {
            TransitionToTitle();
        }
    }

    private void ResetProgression()
    {
        GameManager.instance.ResetProgression();
    }

    /// <summary>
    /// Change to a new state with a loading screen as transition
    /// </summary>
    /// <param name="newState">The new state to change to</param>
    private void PrepareToLoadState(State newState)
    {
        _stateToLoad = newState;

        // Clear scenes already loaded
        _scenesLoading.Clear();

        LoadingGameEvent.instance.Prepare(LoadState);
    }

    private void LoadState()
    {
        ChangeState(_stateToLoad);

        SerializationManager.Save(SaveData.SAVE_NAME, SaveData.current);
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

    public void PrepareGameOver(bool hasWon, int reward)
    {
        SaveData.current.playerProfile.currency += reward;
        SaveData.current.playerProfile.lastMatchWon = hasWon;

        GameManager.instance.Save();
    }

    private void OnDestroy()
    {
        if (GameEvent.instance != null)
        {
            GameEvent.instance.onHomeButtonPressed -= TransitionToHome;
            GameEvent.instance.onStoreButtonPressed -= TransitionToStore;
            GameEvent.instance.onPlayButtonPressed -= TransitionToCharacterSelection;
            GameEvent.instance.onResetButtonPressed -= ResetProgression;
            GameEvent.instance.onCharacterSelected -= TransitionToArena;
            GameEvent.instance.onGameOver -= PrepareGameOver;
        }
    }
}
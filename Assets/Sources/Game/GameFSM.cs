using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFSM : AbstractFSM
{
    [Header("References")]
    public GameObject loadingScreen = null;
    public ProgressBarController loadingBar = null;

    [Header("States")]
    public GameStateWithScene stateHome = new GameStateWithScene();
    public GameStateWithScene stateStore = new GameStateWithScene();

    public GameStateWithScene stateCharacterSelection = new GameStateWithScene();
    public GameStateWithScene stateArena = new GameStateWithScene();
    public GameStateGameOver stateGameOver = new GameStateGameOver();

    [Header("Properties")]
    public float loadingDelay = 0.5f;

    // Transitions to states
    public void TransitionToHome() { LoadState(stateHome); }
    public void TransitionToStore() { LoadState(stateStore); }
    public void TransitionToCharacterSelection() { LoadState(stateCharacterSelection); }
    public void TransitionToArena() { LoadState(stateArena); }
    public void TransitionToGameOver(bool hasWon) { ChangeState(stateGameOver, hasWon); }

    private List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    private float totalSceneProgress = 0f;

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
        loadingScreen.SetActive(true);

        ChangeState(newState);

        StartCoroutine(MonitorLoadingProgress());
    }

    public void RegisterLoadingOperation(AsyncOperation operation)
    {
        scenesLoading.Add(operation);
    }

    /// <summary>
    /// Keep track of current loading operations
    /// </summary>
    /// <returns></returns>
    public IEnumerator MonitorLoadingProgress()
    {
        loadingBar.current = 0;

        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (scenesLoading[i].isDone == false)
            {
                totalSceneProgress = 0f;

                foreach(AsyncOperation operation in scenesLoading)
                {
                    totalSceneProgress += operation.progress;
                }

                // Get percentage of progress
                totalSceneProgress = (totalSceneProgress / scenesLoading.Count) * 100f;

                loadingBar.current = Mathf.RoundToInt(totalSceneProgress);

                yield return null;
            }
        }

        // Always wait for half second to avoid blink on loading screen
        yield return new WaitForSeconds(loadingDelay);

        GameEvent.instance.LoadingEnded();

        loadingScreen.SetActive(false);
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
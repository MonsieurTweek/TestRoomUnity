using UnityEngine;

public class GameFSM : AbstractFSM
{
    [Header("States")]
    public GameStateWithObject stateHome = new GameStateWithObject();
    public GameStateWithObject stateStore = new GameStateWithObject();

    public GameStateWithScene stateCharacterSelection = new GameStateWithScene();
    public GameStateWithScene stateArena = new GameStateWithScene();
    public GameStateGameOver stateGameOver = new GameStateGameOver();

    // Transitions to states
    public void TransitionToHome() { ChangeState(stateHome); }
    public void TransitionToStore() { ChangeState(stateStore); }
    public void TransitionToCharacterSelection() { ChangeState(stateCharacterSelection); }
    public void TransitionToArena() { ChangeState(stateArena); }
    public void TransitionToGameOver(bool hasWon) { ChangeState(stateGameOver, hasWon); }

    private void Start()
    {
        GameEvent.instance.onHomeButtonPressed += TransitionToHome;
        GameEvent.instance.onStoreButtonPressed += TransitionToStore;
        GameEvent.instance.onPlayButtonPressed += TransitionToCharacterSelection;
        GameEvent.instance.onCharacterSelected += TransitionToArena;
        GameEvent.instance.onGameOver += TransitionToGameOver;

        TransitionToHome();
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
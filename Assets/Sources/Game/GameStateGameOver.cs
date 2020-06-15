using System;
using UnityEngine.SceneManagement;

[Serializable]
public class GameStateGameOver : AbstractFSM.State1Param<bool>
{
    private GameSceneIndexes gameScene = GameSceneIndexes.GAME_OVER;
    public bool hasWon { private set; get; }

    public override void Enter(bool hasWon)
    {
        this.hasWon = hasWon;

        UnityEngine.Debug.Log("Load scene " + gameScene);

        SceneManager.LoadSceneAsync((int)gameScene, LoadSceneMode.Additive);
    }

    public override void Exit()
    {
        UnityEngine.Debug.Log("Unload scene " + gameScene);

        SceneManager.UnloadSceneAsync((int)gameScene);
    }
}
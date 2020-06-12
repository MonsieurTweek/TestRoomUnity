using System;
using UnityEngine.SceneManagement;

[Serializable]
public class GameStateWithScene : AbstractFSM.State
{
    public GameSceneIndexes gameScene = GameSceneIndexes.MENU;

    public override void Enter()
    {
        UnityEngine.Debug.Log("Load scene " + gameScene);

        SceneManager.LoadSceneAsync((int)gameScene, LoadSceneMode.Additive);
    }

    public override void Exit()
    {
        UnityEngine.Debug.Log("Unload scene " + gameScene);

        SceneManager.UnloadSceneAsync((int)gameScene);
    }
}
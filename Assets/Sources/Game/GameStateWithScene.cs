using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class GameStateWithScene : AbstractFSM.State
{
    public GameSceneIndexes gameScene = GameSceneIndexes.HOME;

    private List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    public override void Enter()
    {
        ((GameFSM)owner).RegisterLoadingOperation(SceneManager.LoadSceneAsync((int)gameScene, LoadSceneMode.Additive));
    }

    public override void Exit()
    {
        ((GameFSM)owner).RegisterLoadingOperation(SceneManager.UnloadSceneAsync((int)gameScene));
    }
}
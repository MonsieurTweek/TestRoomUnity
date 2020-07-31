using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class GameStateWithScene : AbstractFSM.State
{
    public GameSceneIndexes gameScene = GameSceneIndexes.HOME;
    public AudioClip music = null;

    private List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    public override void Enter()
    {
        ((GameFSM)owner).RegisterLoadingOperation(SceneManager.LoadSceneAsync((int)gameScene, LoadSceneMode.Additive));

        ((GameFSM)owner).StartLoading();

        if (music != null)
        {
            AudioManager.instance.PlayMusic(music);
        }
    }

    public override void Exit()
    {
        ((GameFSM)owner).RegisterLoadingOperation(SceneManager.UnloadSceneAsync((int)gameScene));
    }
}
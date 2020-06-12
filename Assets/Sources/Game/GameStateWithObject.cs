using System;
using UnityEngine;

[Serializable]
public class GameStateWithObject : AbstractFSM.State
{
    public GameObject[] gameObjects = null;

    public override void Enter()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(true);
        }
    }

    public override void Exit()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(false);
        }
    }
}

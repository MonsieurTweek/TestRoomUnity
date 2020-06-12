using System;
using UnityEngine;

[Serializable]
public class GameStateGameOver : AbstractFSM.State1Param<bool>
{
    public GameObject root = null;

    public override void Enter(bool hasWon)
    {
        root.SetActive(true);
    }

    public override void Exit()
    {
        root.SetActive(false);
    }
}
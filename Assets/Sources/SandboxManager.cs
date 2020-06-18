using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SandboxManager : MonoBehaviour
{
    public PlayerFSM player = null;
    public EnemyFSM[] enemies = null;

    private void Start()
    {
        if (player != null)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].Initialize(player);
            }
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaManager : MonoBehaviour
{
    public PlayerFSM player = null;
    public List<EnemyFSM> enemies = new List<EnemyFSM>();

    private void Start()
    {
        CharacterGameEvent.instance.onDie += OnDie;
    }

    private void OnDie(uint id)
    {
        if (player.data.uniqueId == id)
        {
            //TODO : Game Over
            SceneManager.LoadScene(0);
        }
        else
        {
            EnemyFSM enemyToRemove = null;

            for(int i = 0; i < enemies.Count; ++i)
            {
                if (enemies[i].data.uniqueId == id)
                {
                    enemyToRemove = enemies[i];
                    break;
                }
            }

            if (enemyToRemove != null)
            {
                enemies.Remove(enemyToRemove);
            }

            if (enemies.Count == 0)
            {
                // Pause characters
                CharacterGameEvent.instance.PauseRaised(true);

                // Show perks
                PerkGameEvent.instance.DisplayRaised();
            }
        }
    }
}
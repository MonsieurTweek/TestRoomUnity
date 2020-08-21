using System.Collections;
using UnityEngine;

public class SandboxManager : MonoBehaviour
{
    public PlayerFSM player = null;
    public EnemyFSM[] enemies = null;

    private void Start()
    {
        StartCoroutine(Initialize());

        CharacterGameEvent.instance.onDied += OnDie;
    }

    private IEnumerator Initialize()
    {
        yield return new WaitForSeconds(0.25f);

        if (player != null)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].Initialize(player);
            }
        }
    }

    private void OnDie(uint id, int reward)
    {
        EnemyFSM remainingEnemy = null;

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].data.isAlive == true)
            {
                remainingEnemy = enemies[i];
                break;
            }
        }

        if (remainingEnemy == null)
        {
            // You win !
            CharacterGameEvent.instance.Pause(true);
            CharacterGameEvent.instance.OutroStart(player.transform);

            GameEvent.instance.GameOver(true, 1000);
        }
        else if (player.data.uniqueId == id)
        {
            // You lose !
            CharacterGameEvent.instance.OutroStart(remainingEnemy.transform);

            GameEvent.instance.GameOver(false, 500);
        }
    }

    public void KillEnemy()
    {
        foreach (EnemyFSM enemy in enemies)
        {
            enemy.Hit(Mathf.RoundToInt(enemy.data.healthMax * 0.5f));
        }
    }

    public void KillPlayer()
    {
        player.Hit(Mathf.RoundToInt(player.data.healthMax * 0.5f));
    }

    private void OnDestroy()
    {
        CharacterGameEvent.instance.onDied -= OnDie;
    }
}

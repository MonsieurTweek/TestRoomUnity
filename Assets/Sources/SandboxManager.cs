using UnityEngine;

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
}

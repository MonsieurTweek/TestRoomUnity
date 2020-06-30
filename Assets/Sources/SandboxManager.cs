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


#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.K) == true)
        {
            foreach (EnemyFSM enemy in enemies)
            {
                enemy.Hit(Mathf.RoundToInt(enemy.data.healthMax * 0.5f));
            }
        }

        if (Input.GetKeyUp(KeyCode.L) == true)
        {
            player.Hit(Mathf.RoundToInt(player.data.healthMax));
        }
    }
#endif
}

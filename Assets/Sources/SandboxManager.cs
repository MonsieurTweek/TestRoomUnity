using System.Collections;
using UnityEngine;

public class SandboxManager : MonoBehaviour
{
    [Header("Properties")]
    public AudioClip music = null;
    public PlayerFSM player = null;
    public EnemyFSM[] enemies = null;

    public static SandboxManager instance { private set; get; }

    private void Awake()
    {
        // First destroy any existing instance of it
        if (instance != null)
        {
            Destroy(instance);
        }

        // Then reassign a proper one
        instance = this;
    }

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

        AudioManager.instance.PlayMusic(music);
        AudioManager.instance.FadeInMusic();
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
        foreach (EnemyFSM enemy in instance.enemies)
        {
            enemy.Hit(Mathf.RoundToInt(enemy.data.healthMax * 0.5f));
        }
    }

    public void KillPlayer()
    {
        if (instance.player != null)
        {
            instance.player.Hit(Mathf.RoundToInt(instance.player.data.healthMax * 0.5f));
        }
    }

    private void OnDestroy()
    {
        CharacterGameEvent.instance.onDied -= OnDie;
    }
}

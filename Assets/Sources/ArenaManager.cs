using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    [Header("Properties")]
    public PlayerFSM player = null;
    public List<EnemyFSM> enemies = new List<EnemyFSM>();
    public int enemyPerTiers = 3;

    private int _currentReward = 0;
    private int _currentEnemyIndex = 0;
    private uint _lastHitEnemyId = 0;
    private Dictionary<uint, EnemyFSM> _currentEnemies = new Dictionary<uint, EnemyFSM>();
    private List<EnemyFSM> _enemiesToSpawn = new List<EnemyFSM>();

    private void Start()
    {
        CharacterGameEvent.instance.onHit += OnHit;
        CharacterGameEvent.instance.onDied += OnDie;
        PerkGameEvent.instance.onUnlockEnded += OnPerkUnlockEnded;

        StartCoroutine(WaitForIntro());
    }

    // Replace with binding on player intro done
    private IEnumerator WaitForIntro()
    {
        yield return new WaitForSeconds(0.25f);

        PrepareEnemies();

        SpawnNextEnemy();
    }

    private void PrepareEnemies()
    {
        List<EnemyFSM> tierList = new List<EnemyFSM>();

        for (uint i = 1; i <= Enemy.TIERS_MAX; i++)
        {
            tierList.Clear();

            foreach (EnemyFSM enemy in enemies)
            {
                Enemy configuration = (Enemy)enemy.configuration;

                if (configuration.tiers == i)
                {
                    tierList.Add(enemy);
                }
            }

            while(tierList.Count > 0 && _enemiesToSpawn.Count < enemyPerTiers * i)
            {
                int index = Random.Range(0, tierList.Count);

                EnemyFSM enemyToAdd = tierList[index];

                _enemiesToSpawn.Add(enemyToAdd);
                tierList.Remove(enemyToAdd);
            }
        }
    }

    private void SpawnNextEnemy()
    {
        EnemyFSM enemy = Instantiate<EnemyFSM>(_enemiesToSpawn[_currentEnemyIndex], transform);

        enemy.Initialize(player);

        _currentEnemies.Add(enemy.data.uniqueId, enemy);

        // Ensure we have a proper last hit registered
        _lastHitEnemyId = enemy.data.uniqueId;
    }

    private void OnHit(uint id, int health, int damage)
    {
        if (player.data.uniqueId != id)
        {
            _lastHitEnemyId = id;
        }
    }

    private void OnDie(uint id, int reward)
    {
        if (player.data.uniqueId == id)
        {
            // You lose !
            CharacterGameEvent.instance.OutroStart(_currentEnemies[_lastHitEnemyId].transform);

            GameEvent.instance.GameOver(false, _currentReward);
        }
        else
        {
            uint idToRemove = 0;

            foreach(uint uniqueId in _currentEnemies.Keys)
            {
                if (uniqueId == id)
                {
                    _currentReward += reward;
                    idToRemove = uniqueId;
                    break;
                }
            }

            if (idToRemove != 0)
            {
                _currentEnemies.Remove(idToRemove);
            }

            if (_currentEnemies.Count == 0)
            {
                _currentEnemyIndex++;

                // Pause characters
                CharacterGameEvent.instance.Pause(true);

                if (_currentEnemyIndex < _enemiesToSpawn.Count)
                {
                    // Show perks
                    PerkGameEvent.instance.Display();
                }
                else
                {
                    // You win !
                    CharacterGameEvent.instance.Pause(true);
                    CharacterGameEvent.instance.OutroStart(player.transform);

                    GameEvent.instance.GameOver(true, _currentReward);
                }
            }
        }
    }

    private void OnPerkUnlockEnded(uint perkId, Perk perk)
    {
        SpawnNextEnemy();
    }

    private void OnDestroy()
    {
        if (CharacterGameEvent.instance != null)
        {
            CharacterGameEvent.instance.onDied -= OnDie;
        }

        if (PerkGameEvent.instance != null)
        {
            PerkGameEvent.instance.onUnlockEnded -= OnPerkUnlockEnded;
        }
    }

    public void KillEnemy()
    {
        foreach (EnemyFSM enemy in _currentEnemies.Values)
        {
            enemy.Hit(Mathf.RoundToInt(enemy.data.healthMax * 0.5f));
        }
    }

    public void KillPlayer()
    {
        player.Hit(Mathf.RoundToInt(player.data.healthMax * 0.5f));
    }
}
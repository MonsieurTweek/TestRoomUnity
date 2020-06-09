using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    public PlayerFSM player = null;
    public List<EnemyFSM> enemies = new List<EnemyFSM>();

    private int _currentEnemyIndex = 0;
    private Dictionary<uint, EnemyFSM> _currentEnemies = new Dictionary<uint, EnemyFSM>();

    private void Start()
    {
        CharacterGameEvent.instance.onDie += OnDie;
        PerkGameEvent.instance.onPerkSelected += OnPerkSelected;

        SpawnNextEnemy();
    }

    private void SpawnNextEnemy()
    {
        EnemyFSM enemy = Instantiate<EnemyFSM>(enemies[_currentEnemyIndex], Vector3.zero, Quaternion.identity);

        enemy.Initialize(player);

        _currentEnemies.Add(enemy.data.uniqueId, enemy);
    }

    private void OnDie(uint id)
    {
        if (player.data.uniqueId == id)
        {
            //TODO : Game Over
            Debug.Log("Game Over");
        }
        else
        {
            uint idToRemove = 0;

            foreach(uint uniqueId in _currentEnemies.Keys)
            {
                if (uniqueId == id)
                {
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
                // Pause characters
                CharacterGameEvent.instance.PauseRaised(true);

                // Show perks
                PerkGameEvent.instance.DisplayRaised();
            }
        }
    }

    private void OnPerkSelected(uint perkId)
    {
        _currentEnemyIndex++;

        if (_currentEnemyIndex < enemies.Count)
        {
            SpawnNextEnemy();
        }
    }

    private void OnDestroy()
    {
        if (CharacterGameEvent.instance != null)
        {
            CharacterGameEvent.instance.onDie -= OnDie;
        }

        if (PerkGameEvent.instance != null)
        {
            PerkGameEvent.instance.onPerkSelected -= OnPerkSelected;
        }
    }
}
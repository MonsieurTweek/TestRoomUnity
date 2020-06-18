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
        PerkGameEvent.instance.onSelected += OnPerkSelected;

        SpawnNextEnemy();
    }

    private void SpawnNextEnemy()
    {
        EnemyFSM enemy = Instantiate<EnemyFSM>(enemies[_currentEnemyIndex], transform);

        enemy.Initialize(player);

        _currentEnemies.Add(enemy.data.uniqueId, enemy);
    }

    private void OnDie(uint id)
    {
        if (player.data.uniqueId == id)
        {
            // You lose !
            GameEvent.instance.GameOverRaised(false);
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
                _currentEnemyIndex++;

                // Pause characters
                CharacterGameEvent.instance.Pause(true);

                if (_currentEnemyIndex < enemies.Count)
                {
                    // Show perks
                    PerkGameEvent.instance.Display();
                }
                else
                {
                    // You win !
                    GameEvent.instance.GameOverRaised(true);
                }
            }
        }
    }

    private void OnPerkSelected(uint perkId)
    {
        SpawnNextEnemy();
    }

    private void OnDestroy()
    {
        if (CharacterGameEvent.instance != null)
        {
            CharacterGameEvent.instance.onDie -= OnDie;
        }

        if (PerkGameEvent.instance != null)
        {
            PerkGameEvent.instance.onSelected -= OnPerkSelected;
        }
    }
}
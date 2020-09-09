using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    [Header("Properties")]
    public AudioClip music = null;
    public PlayerFSM player = null;
    public int enemyPerTiers = 3;
    public List<EnemyFSM> enemies = new List<EnemyFSM>();
    public List<Transform> spawnPoints = new List<Transform>();

    private int _currentReward = 0;
    private int _currentEnemyIndex = 0;
    private uint _lastHitEnemyId = 0;
    private Dictionary<uint, EnemyFSM> _currentEnemies = new Dictionary<uint, EnemyFSM>();
    private List<EnemyFSM> _enemiesToSpawn = new List<EnemyFSM>();

    private void Start()
    {
        CharacterGameEvent.instance.onHit += OnHit;
        CharacterGameEvent.instance.onDied += OnDie;
        CharacterGameEvent.instance.onIntroStarted += OnIntroStarted;
        CharacterGameEvent.instance.onIntroPaused += OnIntroPaused;
        PerkGameEvent.instance.onUnlockEnded += OnPerkUnlockEnded;

        // Replace with binding on player intro done
        LoadingGameEvent.instance.onLoadingEnded += OnLoadingEnded;
    }

    private void OnLoadingEnded()
    {
        PrepareEnemies();

        SpawnNextEnemy();
    }

    private void OnIntroStarted(Transform _, AbstractCharacterData __)
    {
        AudioManager.instance.FadeOutMusic();
    }

    private void OnIntroPaused()
    {
        AudioManager.instance.PlayMusic(music);
        AudioManager.instance.FadeInMusic();
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
        Transform spawPoint = GetSpawnPoint();
        EnemyFSM enemy = Instantiate<EnemyFSM>(_enemiesToSpawn[_currentEnemyIndex], spawPoint.position, transform.rotation, transform);

        enemy.transform.LookAt(player.transform);
        enemy.Initialize(player);

        _currentEnemies.Add(enemy.data.uniqueId, enemy);

        Quaternion rotation = Quaternion.LookRotation(enemy.transform.position - player.transform.position);
        LeanTween.rotate(player.gameObject, rotation.eulerAngles, 0.5f);

        // Ensure we have a proper last hit registered
        _lastHitEnemyId = enemy.data.uniqueId;
    }

    private Transform GetSpawnPoint()
    {
        Transform bestSpawnPoint = null;
        float currentDistance = 0f;

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            Vector3 directionToPlayer = player.transform.position - spawnPoints[i].position;
            float magnitude = directionToPlayer.magnitude; // Don't use sqrt as not necessary to compare distance

            if (bestSpawnPoint == null || magnitude > currentDistance)
            {
                bestSpawnPoint = spawnPoints[i];
                currentDistance = magnitude;
            }
        }

        return bestSpawnPoint;
    }

    private void OnHit(uint id, CharacterTypeEnum type, int health, int damage)
    {
        // Get only hit from enemies
        if (type == CharacterTypeEnum.ENEMY)
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

                    AudioManager.instance.FadeOutMusic(0.15f);
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

    private void OnPerkUnlockEnded(uint _, Perk __)
    {
        StartCoroutine(WaitForUnlockFeedback());
    }

    private IEnumerator WaitForUnlockFeedback()
    {
        yield return new WaitForSeconds(0.5f);

        SpawnNextEnemy();
    }

    private void OnDestroy()
    {
        if (CharacterGameEvent.instance != null)
        {
            CharacterGameEvent.instance.onHit -= OnHit;
            CharacterGameEvent.instance.onDied -= OnDie;
            CharacterGameEvent.instance.onIntroStarted -= OnIntroStarted;
            CharacterGameEvent.instance.onIntroPaused -= OnIntroPaused;
        }

        if (PerkGameEvent.instance != null)
        {
            PerkGameEvent.instance.onUnlockEnded -= OnPerkUnlockEnded;
        }

        if (LoadingGameEvent.instance != null)
        {
            LoadingGameEvent.instance.onLoadingEnded -= OnLoadingEnded;
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
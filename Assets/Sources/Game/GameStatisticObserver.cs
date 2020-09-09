using UnityEngine;

public class GameStatisticObserver : MonoBehaviour
{
    private int _countEnemyKilled = 0;
    private int _countDamageReceived = 0;
    private int _countDamageInflicted = 0;

    private float _timeOnPlayerLoaded = 0f;
    private float _timeOnIntroEnded = 0f;

    private float _durationEnemy = 0f;

    private void Start()
    {
        LoadingGameEvent.instance.onPlayerLoaded += OnPlayerLoaded;

        GameEvent.instance.onGameOver += OnGameOver;
    }

    private void OnPlayerLoaded()
    {
        _timeOnPlayerLoaded = Time.time;

        CharacterGameEvent.instance.onIntroEnded += OnIntroEnded;

        CharacterGameEvent.instance.onHit += OnHit;
        CharacterGameEvent.instance.onDying += OnDying;
    }

    private void OnIntroEnded()
    {
        _timeOnIntroEnded = Time.time;
    }

    /// <summary>
    /// Keep track of every hit so we can count damage received/inflicted
    /// </summary>
    /// <param name="uniqueId">Unique id of the agent taking damages</param>
    /// <param name="health">Health of the agent taking damages</param>
    /// <param name="damage">Damages received by the agent</param>
    private void OnHit(uint uniqueId, CharacterTypeEnum type, int health, int damage)
    {
        if (type == CharacterTypeEnum.PLAYER)
        {
            _countDamageReceived += damage;
        }
        else if (type == CharacterTypeEnum.ENEMY)
        {
            _countDamageInflicted += damage;
        }
    }

    /// <summary>
    /// Keep track of every death so we can count boss defeated and track how long the fight last
    /// </summary>
    /// <param name="uniqueId">The agent dying unique id</param>
    private void OnDying(uint uniqueId, CharacterTypeEnum type)
    {
        if (type == CharacterTypeEnum.ENEMY)
        {
            _durationEnemy = _durationEnemy + (Time.time - _timeOnIntroEnded);

            _countEnemyKilled++;
        }
    }

    private void OnGameOver(bool hasWon, int rewards)
    {
        // Unbind events
        if (CharacterGameEvent.instance != null)
        {
            CharacterGameEvent.instance.onIntroEnded -= OnIntroEnded;

            CharacterGameEvent.instance.onHit -= OnHit;
            CharacterGameEvent.instance.onDying -= OnDying;
        }

        // Save session statistics
        SaveData.current.playerProfile.sessionStatistics.countEnemyKilled = _countEnemyKilled;
        SaveData.current.playerProfile.sessionStatistics.countDamageInflicted = _countDamageInflicted;
        SaveData.current.playerProfile.sessionStatistics.countDamageReceived = _countDamageReceived;

        SaveData.current.playerProfile.sessionStatistics.timeTotal = Time.time - _timeOnPlayerLoaded;
        SaveData.current.playerProfile.sessionStatistics.timeAverage = _countEnemyKilled > 0 
            ? _durationEnemy / _countEnemyKilled 
            : SaveData.current.playerProfile.sessionStatistics.timeTotal;

        // Save total statistics

        // But first compute previous average with current
        // https://www.statisticshowto.com/combined-mean/
        float totalAverageTimePerEnemy =
            ((SaveData.current.playerProfile.totalStatistics.timeAverage * SaveData.current.playerProfile.totalStatistics.countEnemyKilled)
            + (SaveData.current.playerProfile.sessionStatistics.timeAverage * SaveData.current.playerProfile.sessionStatistics.countEnemyKilled))
            / (SaveData.current.playerProfile.totalStatistics.countEnemyKilled + SaveData.current.playerProfile.sessionStatistics.countEnemyKilled);

        SaveData.current.playerProfile.totalStatistics.countEnemyKilled += _countEnemyKilled;
        SaveData.current.playerProfile.totalStatistics.countDamageInflicted += _countDamageInflicted;
        SaveData.current.playerProfile.totalStatistics.countDamageReceived += _countDamageReceived;

        SaveData.current.playerProfile.totalStatistics.timeAverage = totalAverageTimePerEnemy;
        SaveData.current.playerProfile.totalStatistics.timeTotal += Time.time - _timeOnPlayerLoaded;

        if (hasWon == true)
        {
            SaveData.current.playerProfile.countMatchWon++;
        }
        else
        {
            SaveData.current.playerProfile.countMatchLost++;
        }

        GameManager.instance.Save();
    }

    private void OnDestroy()
    {
        if (LoadingGameEvent.instance != null)
        {
            LoadingGameEvent.instance.onPlayerLoaded -= OnPlayerLoaded;
        }

        if (CharacterGameEvent.instance != null)
        {
            CharacterGameEvent.instance.onIntroEnded -= OnIntroEnded;

            CharacterGameEvent.instance.onHit -= OnHit;
            CharacterGameEvent.instance.onDying -= OnDying;
        }

        if (GameEvent.instance != null)
        {
            GameEvent.instance.onGameOver -= OnGameOver;
        }
    }
}
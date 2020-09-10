using System;

[Serializable]
public class PlayerProfileData
{
    [Serializable]
    public struct PlayerStatistics
    {
        public int countEnemyKilled;
        public int countDamageInflicted;
        public int countDamageReceived;

        public float timeAverage;
        public float timeTotal;
    }

    private int _currency = 10;
    public int currency
    {
        get
        {
            return _currency;
        }

        set
        {
            lastCurrency = _currency;
            _currency = value;
        }
    }

    public int lastCurrency { private set; get; }
    public uint characters = (uint)CharacterEnum.DEFAULT_UNLOCK;
    public uint perks = (uint)PerkEnum.DEFAULT_UNLOCK;

    public bool lastMatchWon = false;
    public int countMatchWon = 0;
    public int countMatchLost = 0;

    public PlayerStatistics sessionStatistics = new PlayerStatistics();
    public PlayerStatistics totalStatistics = new PlayerStatistics();
}

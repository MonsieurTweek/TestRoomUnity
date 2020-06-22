using System;

[Serializable]
public class PlayerProfileData
{
    private int _currency = 999;
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
}

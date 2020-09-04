using System;

[Serializable]
public abstract class Perk : DescriptiveObject
{
    public static readonly string AMOUNT_STRING_KEY = "{:amount}";

    public enum PerkUsage
    {
        INSTANT = 0,
        PERMANENT = 1
    }

    public int amount = 0;
    public PerkEnum type = PerkEnum.NONE;
    public PerkUsage usage = PerkUsage.INSTANT;
    public bool isCumulative = false;
    public bool isUnlock { get { return (SaveData.current.playerProfile.perks & (uint)type) != 0; } }

    protected CharacterFSM _owner = null;

    public virtual void Unlock(CharacterFSM character)
    {
        _owner = character;
    }

    public virtual void Evaluate(CharacterFSM character) { }
}
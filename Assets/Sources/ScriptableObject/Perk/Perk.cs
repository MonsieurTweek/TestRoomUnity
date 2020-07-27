using System;

[Serializable]
public abstract class Perk : DescriptiveObject
{
    public static readonly string AMOUNT_STRING_KEY = "{:amount}";

    public enum PerkType
    {
        INSTANT = 0,
        PERMANENT = 1
    }

    public int amount = 0;
    public PerkType type = PerkType.INSTANT;
    public bool isCumulative = false;

    protected CharacterFSM _owner = null;

    public virtual void Unlock(CharacterFSM character)
    {
        _owner = character;
    }

    public virtual void Evaluate(CharacterFSM character) { }
}
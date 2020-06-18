using System;
using UnityEngine;

[Serializable]
public abstract class Perk : DescriptiveObject
{
    public enum PerkType
    {
        INSTANT = 0,
        PERMANENT = 1
    }

    public GameObject fx = null;
    public int amount = 0;
    public PerkType type = PerkType.INSTANT;

    protected CharacterFSM _owner = null;

    public virtual void Unlock(CharacterFSM character)
    {
        _owner = character;
    }

    public virtual void Evaluate(CharacterFSM character) { }
}
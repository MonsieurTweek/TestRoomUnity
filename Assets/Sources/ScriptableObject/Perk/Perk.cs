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

    public virtual void Unlock(PlayerFSM player)
    {
        GameObject.Instantiate(fx, player.transform);
    }

    public virtual void Evaluate(PlayerFSM player) { }
}
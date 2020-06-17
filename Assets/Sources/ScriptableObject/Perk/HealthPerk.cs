using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPerk", menuName = "ScriptableObjects/Perks/Health", order = 2)]
[Serializable]
public class HealthPerk : Perk
{
    public override void Unlock(PlayerFSM player)
    {
        base.Unlock(player);

        player.data.BuffHealth(amount, true);
    }
}
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPerk", menuName = "ScriptableObjects/Perks/Health")]
[Serializable]
public class HealthPerk : Perk
{
    public override void Unlock(CharacterFSM character)
    {
        base.Unlock(character);

        bool isPermanent = type == PerkType.PERMANENT;

        character.data.BuffHealth(amount, isPermanent);

    }
}
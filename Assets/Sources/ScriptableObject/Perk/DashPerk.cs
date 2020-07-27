using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DashPerk", menuName = "ScriptableObjects/Perks/Dash")]
[Serializable]
public class DashPerk : Perk
{
    public override void Unlock(CharacterFSM character)
    {
        base.Unlock(character);

        ((PlayerData)character.data).UnlockAbility(PlayerAbilityEnum.DASH);
    }
}
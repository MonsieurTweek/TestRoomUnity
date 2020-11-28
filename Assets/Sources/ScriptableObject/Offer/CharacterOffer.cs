using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Offer", menuName = "ScriptableObjects/Store/Character Offer", order = 3)]
[Serializable]
public class CharacterOffer : StoreOffer
{
    public CharacterEnum character = CharacterEnum.NONE;

    public override StoreOfferEnum GetCategory()
    {
        return StoreOfferEnum.CHARACTER;
    }

    public override string GetContentName()
    {
        return character.ToString();
    }

    public override void Buy()
    {
        base.Buy();

        SaveData.current.playerProfile.characters |= (uint)character;
    }
}

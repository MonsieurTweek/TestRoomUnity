using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Offer", menuName = "ScriptableObjects/Store Offer - Character", order = 5)]
[Serializable]
public class CharacterOffer : StoreOffer
{
    public CharacterEnum character = CharacterEnum.NONE;

    public override void Buy()
    {
        base.Buy();

        SaveData.current.playerProfile.characters |= (uint)character;
    }
}

using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Offer", menuName = "ScriptableObjects/Store/Perk Offer", order = 3)]
[Serializable]
public class PerkOffer : StoreOffer
{
    public PerkEnum perk = PerkEnum.NONE;

    public override StoreOfferEnum GetCategory()
    {
        return StoreOfferEnum.PERK;
    }

    public override string GetContentName()
    {
        return perk.ToString();
    }

    public override void Buy()
    {
        base.Buy();

        SaveData.current.playerProfile.perks |= (uint)perk;
    }
}

using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Offer", menuName = "ScriptableObjects/Store/Currency Offer", order = 2)]
[Serializable]
public class CurrencyOffer : StoreOffer
{
    public override void Buy()
    {
        base.Buy();

        SaveData.current.playerProfile.currency += amount;
    }
}

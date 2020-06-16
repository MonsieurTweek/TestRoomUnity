using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Offer", menuName = "ScriptableObjects/Store Offer", order = 3)]
[Serializable]
public class StoreOffer : DescriptiveObject
{
    public int price = 1000;
    public int amount = 1;

    public virtual void Buy()
    {
        SaveData.current.playerProfile.currency -= price;
    }
}
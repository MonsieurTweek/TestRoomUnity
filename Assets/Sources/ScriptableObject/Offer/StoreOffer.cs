using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Offer", menuName = "ScriptableObjects/Store/General Offer", order = 1)]
[Serializable]
public class StoreOffer : DescriptiveObject
{
    public int price = 1000;
    public int amount = 1;
    public AudioClip purchaseSound = null;

    public virtual void Buy()
    {
        SaveData.current.playerProfile.currency -= price;
    }
}
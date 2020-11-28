using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public partial class AnalyticsManager : MonoBehaviour
{
    public static void GameStart()
    {
        AnalyticsEvent.GameStart();
    }

    public static void GameOver(bool hasWon)
    {
        Dictionary<string, object> parameters = instance.GetParameters();

        parameters["win"] = hasWon;

        AnalyticsEvent.GameOver();
    }

    public static void LevelStart(int index)
    {
        AnalyticsEvent.LevelStart(index);
    }

    public static void LevelComplete(int index)
    {
        AnalyticsEvent.LevelComplete(index);
    }

    public static void LevelFail(int index)
    {
        AnalyticsEvent.LevelFail(index);
    }

    public static void FirstInteraction()
    {
        AnalyticsEvent.FirstInteraction();
    }

    public static void StoreOpened()
    {
        Dictionary<string, object> parameters = instance.GetParameters();

        parameters["currency"] = SaveData.current.playerProfile.currency;

        AnalyticsEvent.StoreOpened(StoreType.Soft, parameters);
    }

    public static void StoreItemClick(StoreOffer offer, bool canPurchase)
    {
        Dictionary<string, object> parameters = instance.GetParameters();

        parameters["category"] = offer.GetCategory();
        parameters["content"] = offer.GetContentName();

        parameters["price"] = offer.price;
        parameters["amount"] = offer.amount;

        parameters["can_purchase"] = canPurchase;

        AnalyticsEvent.StoreItemClick(StoreType.Soft, offer.title, offer.title, parameters);
    }

    public static void StoreItemAcquired(StoreOffer offer)
    {
        Dictionary<string, object> parameters = instance.GetParameters();

        AnalyticsEvent.ItemAcquired(AcquisitionType.Soft, "store", (float)offer.amount, offer.title, (float)SaveData.current.playerProfile.currency, offer.GetContentName(), null, null, parameters);
    }
}
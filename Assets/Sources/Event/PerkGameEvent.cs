using System;
using UnityEngine;

public class PerkGameEvent : MonoBehaviour
{
    private static PerkGameEvent _instance = null;
    public static PerkGameEvent instance { get { return _instance; } set { _instance = value; } }

    private void Awake()
    {
        _instance = this;
    }

    public event Action onDisplayed;
    public void Display()
    {
        if (onDisplayed != null)
        {
            onDisplayed();
        }
    }

    public event Action<uint, Perk> onUnlockStarted;
    public void StartUnlock(CardData data)
    {
        if (onUnlockStarted != null)
        {
            onUnlockStarted(data.uniqueId, (Perk)data.descriptiveObject);
        }
    }

    public event Action<uint, Perk> onUnlockEnded;
    public void EndUnlock(CardData data)
    {
        if (onUnlockEnded != null)
        {
            onUnlockEnded(data.uniqueId, (Perk)data.descriptiveObject);
        }
    }
}
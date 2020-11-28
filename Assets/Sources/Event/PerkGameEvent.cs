using System;
using System.Collections.Generic;
using UnityEngine;

public class PerkGameEvent : MonoBehaviour
{
    private static PerkGameEvent _instance = null;
    public static PerkGameEvent instance { get { return _instance; } set { _instance = value; } }

    private void Awake()
    {
        _instance = this;
    }

    public event Action onDisplayStarted;
    public void StartDisplay()
    {
        if (onDisplayStarted != null)
        {
            onDisplayStarted();
        }
    }

    public event Action<List<Perk>> onDisplayEnded;
    public void EndDisplay(List<Perk> perks)
    {
        if (onDisplayEnded != null)
        {
            onDisplayEnded(perks);
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
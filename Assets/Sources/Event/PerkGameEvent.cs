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

    public event Action<uint> onSelected;
    public void Select(CardData data)
    {
        if (onSelected != null)
        {
            onSelected(data.uniqueId);
        }
    }

    public event Action<uint, Perk> onUnlocked;
    public void Unlock(CardData data)
    {
        if (onUnlocked != null)
        {
            onUnlocked(data.uniqueId, (Perk)data.descriptiveObject);
        }
    }
}
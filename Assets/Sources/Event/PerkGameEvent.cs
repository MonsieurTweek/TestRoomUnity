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

    public event Action onDisplay;
    public void DisplayRaised()
    {
        if (onDisplay != null)
        {
            onDisplay();
        }
    }

    public event Action<uint> onPerkSelected;
    public void PerkSelectedRaised(CardData data)
    {
        if (onPerkSelected != null)
        {
            onPerkSelected(data.uniqueId);
        }
    }
}
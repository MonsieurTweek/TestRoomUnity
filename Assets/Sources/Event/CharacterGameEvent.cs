using System;
using UnityEngine;

public class CharacterGameEvent : MonoBehaviour
{
    private static CharacterGameEvent _instance = null;
    public static CharacterGameEvent instance { get { return _instance; } set { _instance = value; } }

    private void Awake()
    {
        _instance = this;
    }

    public event Action<bool> onPause;
    public void PauseRaised(bool isPauseEnabled)
    {
        if (onPause != null)
        {
            onPause(isPauseEnabled);
        }
    }

    public event Action<uint, int, int> onHit;
    public void HitRaised(AbstractCharacterData data, int damage)
    {
        if (onHit != null)
        {
            onHit(data.uniqueId, data.health, damage);
        }
    }

    public event Action<uint> onDie;
    public void DieRaised(AbstractCharacterData data)
    {
        if (onDie != null)
        {
            onDie(data.uniqueId);
        }
    }

    public event Action<uint, int, int> onTargetSelected;
    public void TargetSelectedRaised(AbstractCharacterData data)
    {
        if (onTargetSelected != null)
        {
            onTargetSelected(data.uniqueId, data.health, data.healthMax);
        }
    }

    public event Action<uint> onTargetDeselected;
    public void TargetDeselectedRaised(AbstractCharacterData data)
    {
        if (onTargetDeselected != null)
        {
            onTargetDeselected(data.uniqueId);
        }
    }
}
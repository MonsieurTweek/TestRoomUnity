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
    public void Pause(bool isPauseEnabled)
    {
        if (onPause != null)
        {
            onPause(isPauseEnabled);
        }
    }

    public event Action<uint, float> onDashCompleted;
    public void CompleteDash(uint characterUniqueId, float cooldown)
    {
        if (onDashCompleted != null)
        {
            onDashCompleted(characterUniqueId, cooldown);
        }
    }

    /// <summary>
    /// When a character is hit BY somebody else
    /// </summary>
    public event Action<uint, int, int> onHit;
    public void Hit(AbstractCharacterData target, int damage)
    {
        if (onHit != null)
        {
            onHit(target.uniqueId, target.health, damage);
        }
    }

    /// <summary>
    /// When a character is hitting somebody else
    /// </summary>
    public event Action<uint, uint> onHitting;
    public void Hitting(AbstractCharacterData origin, AbstractCharacterData target)
    {
        if (onHitting != null)
        {
            onHitting(origin.uniqueId, target.uniqueId);
        }
    }

    public event Action<uint> onDying;
    public void Die(AbstractCharacterData target)
    {
        if (onDying != null)
        {
            onDying(target.uniqueId);
        }
    }

    public event Action<uint, int> onDied;
    public void Died(AbstractCharacterData target)
    {
        if (onDied != null)
        {
            onDied(target.uniqueId, target.GetReward());
        }
    }

    public event Action<uint, float> onStunned;
    public void Stun(uint targetUniqueId, float duration)
    {
        if (onStunned != null)
        {
            onStunned(targetUniqueId, duration);
        }
    }

    public event Action<uint, float> onFrozen;
    public void Freeze(uint targetUniqueId, float duration)
    {
        if (onFrozen != null)
        {
            onFrozen(targetUniqueId, duration);
        }
    }

    public event Action<uint, float> onPoisonned;
    public void Poison(uint targetUniqueId, float duration)
    {
        if (onPoisonned != null)
        {
            onPoisonned(targetUniqueId, duration);
        }
    }

    public event Action<uint, int, int> onTargetSelected;
    public void SelectTarget(AbstractCharacterData data)
    {
        if (onTargetSelected != null)
        {
            onTargetSelected(data.uniqueId, data.health, data.healthMax);
        }
    }

    public event Action<uint> onTargetDeselected;
    public void DeselectTarget(AbstractCharacterData data)
    {
        if (onTargetDeselected != null)
        {
            onTargetDeselected(data.uniqueId);
        }
    }

    public event Action<Transform, AbstractCharacterData> onIntroStarted;
    public void IntroStart(Transform target, AbstractCharacterData data)
    {
        if (onIntroStarted != null)
        {
            onIntroStarted(target, data);
        }
    }

    public event Action onIntroEnded;
    public void IntroEnd()
    {
        if (onIntroEnded != null)
        {
            onIntroEnded();
        }
    }
}
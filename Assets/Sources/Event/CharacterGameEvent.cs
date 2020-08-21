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

    public event Action<PlayerData> onPlayerLoading;
    public void LoadingPlayer(PlayerData data)
    {
        if (onPlayerLoading != null)
        {
            onPlayerLoading(data);
        }
    }

    public event Action onPlayerLoaded;
    public void CompleteLoadingPlayer()
    {
        if (onPlayerLoaded != null)
        {
            onPlayerLoaded();
        }
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
    public event Action<uint, CharacterTypeEnum, int, int> onHit;
    public void Hit(AbstractCharacterData target, int damage)
    {
        if (onHit != null)
        {
            onHit(target.uniqueId, target.type, target.health, damage);
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

    public event Action<uint, CharacterTypeEnum> onDying;
    public void Die(AbstractCharacterData target)
    {
        if (onDying != null)
        {
            onDying(target.uniqueId, target.type);
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

    public event Action<uint> onStunned;
    public void Stun(uint targetUniqueId)
    {
        if (onStunned != null)
        {
            onStunned(targetUniqueId);
        }
    }

    public event Action<uint> onFrozen;
    public void Freeze(uint targetUniqueId)
    {
        if (onFrozen != null)
        {
            onFrozen(targetUniqueId);
        }
    }

    public event Action<uint> onPoisonned;
    public void Poison(uint targetUniqueId)
    {
        if (onPoisonned != null)
        {
            onPoisonned(targetUniqueId);
        }
    }

    public event Action<uint, float> onEnergyUpdated;
    public void UpdateEnergy(uint targetUniqueId, float energy)
    {
        if (onEnergyUpdated != null)
        {
            onEnergyUpdated(targetUniqueId, energy);
        }
    }

    public event Action<uint> onOutOfEnergy;
    public void OutOfEnergy(uint targetUniqueId)
    {
        if (onOutOfEnergy != null)
        {
            onOutOfEnergy(targetUniqueId);
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

    public event Action onIntroPaused;
    public void IntroPause()
    {
        if (onIntroPaused != null)
        {
            onIntroPaused();
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

    public event Action<Transform> onOutroStarted;
    public void OutroStart(Transform target)
    {
        if (onOutroStarted != null)
        {
            onOutroStarted(target);
        }
    }

    public event Action onOutroPlaying;
    public void OutroPlay()
    {
        if (onOutroPlaying != null)
        {
            onOutroPlaying();
        }
    }
}
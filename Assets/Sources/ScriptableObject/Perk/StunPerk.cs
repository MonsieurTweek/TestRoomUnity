using System;
using UnityEngine;

[CreateAssetMenu(fileName = "StunPerk", menuName = "ScriptableObjects/Perks/Stun")]
[Serializable]
public class StunPerk : Perk
{
    public override void Unlock(CharacterFSM character)
    {
        base.Unlock(character);

        CharacterGameEvent.instance.onHitting += OnHitting;
    }

    public void OnHitting(uint originId, uint targetId)
    {
        if (originId == _owner.data.uniqueId)
        {
            CharacterGameEvent.instance.Stun(targetId, amount);
        }
    }

    private void OnDestroy()
    {
        if (CharacterGameEvent.instance != null)
        {
            CharacterGameEvent.instance.onHitting -= OnHitting;
        }
    }
}
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "FreezePerk", menuName = "ScriptableObjects/Perks/Freeze")]
[Serializable]
public class FreezePerk : Perk
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
            CharacterGameEvent.instance.Freeze(targetId, amount);
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
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
        bool shouldApply = UnityEngine.Random.Range(0f, 100f) < amount;

        if (originId == _owner.data.uniqueId && shouldApply == true)
        {
            CharacterGameEvent.instance.Freeze(targetId);
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
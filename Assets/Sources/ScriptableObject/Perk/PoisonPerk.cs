using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PoisonPerk", menuName = "ScriptableObjects/Perks/Poison")]
[Serializable]
public class PoisonPerk : Perk
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
            CharacterGameEvent.instance.Poison(targetId);
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
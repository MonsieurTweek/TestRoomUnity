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
        bool shouldApply = UnityEngine.Random.Range(0, 100) < amount;

        if (originId == _owner.data.uniqueId && shouldApply == true)
        {
            CharacterGameEvent.instance.Stun(targetId);
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
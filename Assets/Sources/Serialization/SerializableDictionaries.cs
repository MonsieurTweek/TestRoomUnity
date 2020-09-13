using System;
using System.Collections.Generic;
using UnityEngine;

public class SerializableDictionaries : MonoBehaviour
{
    [Serializable]
    public class EffectListStorage : SerializableDictionary.Storage<List<VisualEffectController>> { }

    [Serializable]
    public class EffectsByAttackType : SerializableDictionary<CharacterStateAttack.AttackType, List<VisualEffectController>, EffectListStorage> { }

    [Serializable]
    public class StatusByAttackType : SerializableDictionary<CharacterStateAttack.AttackType, CharacterStatusEnum> { }

    [Serializable]
    public class DamageByAttackType : SerializableDictionary<CharacterStateAttack.AttackType, int> { }

    [Serializable]
    public class HealByAttackType : SerializableDictionary<CharacterStateAttack.AttackType, int> { }
}

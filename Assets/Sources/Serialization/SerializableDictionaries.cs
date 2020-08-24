using System;
using System.Collections.Generic;
using UnityEngine;

public class SerializableDictionaries : MonoBehaviour
{
    [Serializable]
    public class EffectListStorage : SerializableDictionary.Storage<List<VisualEffectController>> { }

    [Serializable]
    public class EffectsByAttackType : SerializableDictionary<CharacterStateAttack.AttackType, List<VisualEffectController>, EffectListStorage> { }
}

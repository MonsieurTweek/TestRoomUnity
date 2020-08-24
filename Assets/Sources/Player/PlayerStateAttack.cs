using System;
using System.Collections.Generic;

[Serializable]
public class PlayerStateAttack : CharacterStateAttack
{
    private int _currentFxIndex = 0;
    private SerializableDictionaries.EffectsByAttackType _effects = null;

    public void SetAttackEffects(SerializableDictionaries.EffectsByAttackType effects)
    {
        _effects = effects;
    }
    
    public void OnAttackPlayFx()
    {
        if (_effects != null)
        {
            UnityEngine.Assertions.Assert.IsTrue(_effects.ContainsKey(type) && _effects[type].Count > 0, "Can't find any effects for " + type);

            if (_currentFxIndex < _effects[type].Count)
            {
                _effects[type][_currentFxIndex].Reset((uint)CharacterStateEnum.ATTACK);

                _currentFxIndex++;
            }

            if (_currentFxIndex >= _effects[type].Count)
            {
                _currentFxIndex = 0;
            }
        }
    }

    public override void Exit()
    {
        List<VisualEffectController> fxByState = character.fxController.GetFxByState(flag);

        character.fxController.StopFx(fxByState);
    }
}
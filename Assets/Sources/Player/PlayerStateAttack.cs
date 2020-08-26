using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStateAttack : CharacterStateAttack
{
    private const string ANIMATION_PARAM_COMBO = "Combo";

    public bool isComboAvailable { private set; get; }
    public bool isComboFinalStep { private set; get; }

    private int _currentFxIndex = 0;
    private int _currentComboCount = 0;
    private AttackType _currentComboType = AttackType.NONE;
    private SerializableDictionaries.EffectsByAttackType _effects = null;

    public void SetAttackEffects(SerializableDictionaries.EffectsByAttackType effects)
    {
        _effects = effects;
    }

    public override void Enter(int type)
    {
        base.Enter(type);

        character.gearController.PlayAttackFx();
    }

    public void ToggleCombo()
    {
        isComboAvailable = !isComboAvailable && _currentComboCount < 2;

        if (isComboAvailable == true)
        {
            Time.timeScale = 0.5f;
            character.gearController.PlayComboFx();
        }
        else
        {
            Time.timeScale = 1;
            character.gearController.StopComboFx();
        }
    }

    public void TriggerCombo(AttackType type)
    {
        // First combo step, save current type
        if (_currentComboType == AttackType.NONE)
        {
            _currentComboType = this.type;
        }

        // Trigger a new animation on the end of this one
        character.animator.SetTrigger(ANIMATION_PARAM_COMBO);

        // Reset current fx and keep track of the combo step
        _currentFxIndex = 0;
        _currentComboCount++;

        // Switch type to the new one
        this.type = _currentComboCount < 2 ? type : 
            // Or to the proper final combo
            _currentComboType == AttackType.ALT_1 ? AttackType.COMBO_1_FINAL : AttackType.COMBO_2_FINAL;

        if (this.type == AttackType.COMBO_1_FINAL || this.type == AttackType.COMBO_2_FINAL)
        {
            character.animator.applyRootMotion = true;

            isComboFinalStep = true;
        }

        isComboFinalStep = false;
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
        base.Exit();

        List<VisualEffectController> fxByState = character.fxController.GetFxByState(flag);

        character.gearController.StopAttackFx();
        character.fxController.StopFx(fxByState);

        // Reset combo so we are sure it won't trigger not on purpose
        ResetCombo();
    }

    private void ResetCombo()
    {
        Time.timeScale = 1;

        _currentFxIndex = 0;
        _currentComboType = AttackType.NONE;
        _currentComboCount = 0;

        character.gearController.StopComboFx();

        isComboAvailable = false;
        isComboFinalStep = false;
    }
}
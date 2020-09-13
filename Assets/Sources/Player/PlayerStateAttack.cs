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
    private SerializableDictionaries.StatusByAttackType _status = null;

    public void SetAttackEffects(SerializableDictionaries.EffectsByAttackType effects)
    {
        _effects = effects;
    }

    public void SetAttackStatus(SerializableDictionaries.StatusByAttackType status)
    {
        _status = status;
    }

    public override void Enter(int type)
    {
        base.Enter(type);

        character.gearController.PlayAttackFx();

        // If there is a damage override on the configuration of this attack, we use it
        character.data.SetDamageByAttackType((AttackType)type);

    }

    public void ToggleCombo()
    {
        isComboAvailable = !isComboAvailable && _currentComboCount < 2;

        if (isComboAvailable == true)
        {
            ShowComboFeedback();
        }
        else
        {
            HideComboFeedback();
        }
    }
    
    private void ShowComboFeedback()
    {
        Time.timeScale = 0.5f;
        character.gearController.PlayComboFx();
    }

    private void HideComboFeedback()
    {
        Time.timeScale = 1;
        character.gearController.StopComboFx();
    }

    public void TriggerCombo(AttackType type)
    {
        // Trigger a new animation on the end of this one
        character.animator.SetTrigger(ANIMATION_PARAM_COMBO);

        // Reset current fx and keep track of the combo step
        _currentFxIndex = 0;
        _currentComboCount++;

        // Switch type 
        _currentComboType = _currentComboCount == 1 
            // for the first step to the corresponding combo
            ? this.type == AttackType.ALT_1 ? AttackType.COMBO_1 : AttackType.COMBO_2
            // Or for the last step to the proper final combo
            : this.type == AttackType.ALT_1 ? AttackType.COMBO_1_FINAL : AttackType.COMBO_2_FINAL;

        if (_currentComboType == AttackType.COMBO_1_FINAL || _currentComboType == AttackType.COMBO_2_FINAL)
        {
            character.animator.applyRootMotion = true;
            isComboFinalStep = true;
        }
        else
        {
            isComboFinalStep = false;
        }

        HideComboFeedback();
    }
    
    public void OnAttackPlayFx()
    {
        AttackType currentType = _currentComboType == AttackType.NONE ? type : _currentComboType;

        if (_effects != null)
        {
            UnityEngine.Assertions.Assert.IsTrue(_effects.ContainsKey(currentType) && _effects[currentType].Count > 0, "Can't find any effects for " + currentType);

            if (_currentFxIndex < _effects[currentType].Count)
            {
                _effects[currentType][_currentFxIndex].Reset((uint)CharacterStateEnum.ATTACK);

                _currentFxIndex++;
            }

            if (_currentFxIndex >= _effects[currentType].Count)
            {
                _currentFxIndex = 0;
            }
        }
    }

    public Boolean HasStatus(CharacterStatusEnum status)
    {
        AttackType currentType = _currentComboType == AttackType.NONE ? type : _currentComboType;

        if (_status == null)
        {
            return false;
        }

        try
        {
            UnityEngine.Assertions.Assert.IsTrue(_status.ContainsKey(currentType), "Can't find any status for " + currentType);

            return _status[currentType] == status;
        } catch(Exception e)
        {
            // The current AttackType doesn't have a status defined. Do nothing
            return false;
        }

    }
    
    public override void Exit()
    {
        base.Exit();

        List<VisualEffectController> fxByState = character.fxController.GetFxByState(flag);

        character.gearController.StopAttackFx();
        character.fxController.StopFx(fxByState);
        type = AttackType.NONE;

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
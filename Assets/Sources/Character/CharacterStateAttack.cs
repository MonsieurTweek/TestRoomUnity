using System;

[Serializable]
public class CharacterStateAttack : CharacterFSM.CharacterState1Param<bool>
{
    private const string ANIMATION_PARAM_ATTACK_LIGHT = "AttackLight";
    private const string ANIMATION_PARAM_ATTACK_HEAVY = "AttackHeavy";

    public bool isHeavy { private set; get; }
    public bool isEnabled { private set; get; }

    public override void Enter(bool isHeavy)
    {
        this.isHeavy = isHeavy;

        character.animator.SetTrigger(isHeavy == true ? ANIMATION_PARAM_ATTACK_HEAVY : ANIMATION_PARAM_ATTACK_LIGHT);
    }

    public virtual void OnUpdateAttackSpeed(float speed)
    {
        character.animator.speed = speed;
    }

    public virtual void OnToggleAttack()
    {
        isEnabled = !isEnabled;
    }
}

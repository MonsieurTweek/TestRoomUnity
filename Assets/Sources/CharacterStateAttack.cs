using System;

[Serializable]
public class CharacterStateAttack : CharacterFSM.State1Param<bool>
{
    private const string ANIMATION_PARAM_ATTACK_LIGHT = "AttackLight";
    private const string ANIMATION_PARAM_ATTACK_HEAVY = "AttackHeavy";

    public bool isHeavy { private set; get; }

    public override void Enter(bool isHeavy)
    {
        this.isHeavy = isHeavy;

        owner.animator.SetTrigger(isHeavy == true ? ANIMATION_PARAM_ATTACK_HEAVY : ANIMATION_PARAM_ATTACK_LIGHT);
    }
}

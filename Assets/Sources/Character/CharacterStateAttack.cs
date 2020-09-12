using System;
using UnityEngine;

[Serializable]
public class CharacterStateAttack : CharacterFSM.CharacterState1Param<int>
{
    public enum AttackType
    {
        NONE = 0,

        ALT_1 = 1,
        ALT_2 = 2,
        ALT_3 = 3,

        COMBO_1 = 10,
        COMBO_2 = 11,

        COMBO_1_FINAL = 100,
        COMBO_2_FINAL = 101,
    }

    private const string ANIMATION_PARAM_ATTACK = "Attack";
    private const string ANIMATION_PARAM_ATTACK_TYPE = "AttackType";

    public Transform anchor = null;
    public AttackType type = AttackType.NONE;

    public bool isEnabled { private set; get; }

    public override void Enter(int type)
    {
        isEnabled = false;
        this.type = (AttackType)type;

        character.animator.SetTrigger(ANIMATION_PARAM_ATTACK);
        character.animator.SetInteger(ANIMATION_PARAM_ATTACK_TYPE, type);
    }

    public void OnAttackSendProjectile(UnityEngine.Object prefab, bool isRooted)
    {
        GameObject gameObject = GamePoolManager.instance.UseFromPool(prefab.name);

        gameObject.transform.position = isRooted == true ? character.transform.position : anchor.position;
        gameObject.transform.rotation = character.transform.rotation;

        GearController currentProjectile = gameObject.GetComponent<GearController>();

        currentProjectile.Attach(character);

        gameObject.SetActive(true);
    }

    public virtual void OnUpdateAttackSpeed(float speed)
    {
        character.animator.speed = speed;
    }

    public virtual void OnToggleAttack()
    {
        isEnabled = !isEnabled;
    }

    public override void Exit()
    {
        character.animator.speed = 1f;
        character.animator.applyRootMotion = false;
    }
}
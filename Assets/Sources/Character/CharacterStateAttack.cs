using System;
using UnityEngine;

[Serializable]
public class CharacterStateAttack : CharacterFSM.CharacterState1Param<bool>
{
    private const string ANIMATION_PARAM_ATTACK_LIGHT = "AttackLight";
    private const string ANIMATION_PARAM_ATTACK_HEAVY = "AttackHeavy";

    public Transform anchor = null;

    public bool isHeavy { private set; get; }
    public bool isEnabled { private set; get; }

    public override void Enter(bool isHeavy)
    {
        isEnabled = false;
        this.isHeavy = isHeavy;

        character.animator.SetTrigger(isHeavy == true ? ANIMATION_PARAM_ATTACK_HEAVY : ANIMATION_PARAM_ATTACK_LIGHT);
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
}

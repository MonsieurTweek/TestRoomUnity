using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines what to do when enemy attacks
/// </summary>
[Serializable]
public class EnemyStateAttack : CharacterStateAttack
{
    public Transform anchor = null;

    public void OnAttackSendProjectile(UnityEngine.Object projectile, bool isRooted)
    {
        GameObject gameObject = GamePoolManager.instance.UseFromPool(projectile.name);

        gameObject.transform.position = isRooted == true ? character.transform.position : anchor.position;
        gameObject.transform.rotation = character.transform.rotation;

        GearController currentProjectile = gameObject.GetComponent<GearController>();

        currentProjectile.Attach(character);
    }

    public override void Enter(bool isHeavy)
    {
        character.animator.applyRootMotion = true;

        base.Enter(isHeavy);
    }

    public override void Exit()
    {
        character.animator.applyRootMotion = false;
        character.animator.speed = 1f;
    }
}
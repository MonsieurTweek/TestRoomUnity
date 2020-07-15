using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStateAttack : CharacterStateAttack
{
    public Transform anchor = null;

    public void OnAttackSendProjectile(UnityEngine.Object projectile, bool isRooted)
    {
        GameObject gameObject = GamePoolManager.instance.UseFromPool(projectile.name);

        gameObject.transform.position = isRooted == true ? character.transform.position : anchor.position;
        gameObject.transform.rotation = character.transform.rotation;

        GearController currentProjectile = gameObject.GetComponent<GearController>();

        currentProjectile.Attach(character);

        gameObject.SetActive(true);
    }

    public override void Exit()
    {
        // TODO : Keep fx back to the pool instead
        // GameObject.Destroy(_currentFx);

        List<VisualEffectController> fxByState = character.fxController.GetFxByState(flag);

        character.fxController.StopFx(fxByState);
    }
}
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

    private GameObject _currentFx = null;

    private List<GameObject> _fxInPool = new List<GameObject>();
    private List<GameObject> _fxInUse = new List<GameObject>();

    public void OnAttackSendProjectile(UnityEngine.Object projectile, bool isRooted)
    {
        GameObject gameObject = InstantiateObject(projectile, isRooted);

        GearController currentProjectile = gameObject.GetComponent<GearController>();

        currentProjectile.Attach(character);
    }

    public void OnAttackPlayFx(UnityEngine.Object fx, bool isRooted)
    {
        if (fx != null && (isRooted == true || anchor != null))
        {
            if (_fxInUse.Count >= _fxInPool.Count)
            {
                _currentFx = InstantiateObject(fx, isRooted);
                _fxInUse.Add(_currentFx);
            }
            else
            {
                // TODO : Use from pool
            }
        }
    }

    private GameObject InstantiateObject(UnityEngine.Object prefab, bool isRooted)
    {
        GameObject gameObject = GameObject.Instantiate<GameObject>((GameObject)prefab);

        gameObject.transform.position = isRooted == true ? character.transform.position : anchor.position;
        gameObject.transform.rotation = character.transform.rotation;

        return gameObject;

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
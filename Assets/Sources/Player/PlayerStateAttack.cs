using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStateAttack : CharacterStateAttack
{
    public Transform anchor = null;

    private GameObject _currentFx = null;

    private List<GameObject> _fxInPool = new List<GameObject>();
    private List<GameObject> _fxInUse = new List<GameObject>();

    public void OnAttackSendProjectile(UnityEngine.Object projectile, bool isRooted)
    {
        GameObject gameObject = InstantiateObject(projectile, isRooted);

        GearController currentProjectile = gameObject.GetComponent<GearController>();

        currentProjectile.Attach(owner);
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
        
        gameObject.transform.position = isRooted == true ? owner.transform.position : anchor.position;
        gameObject.transform.rotation = owner.transform.rotation;

        return gameObject;
        
    }

    public override void Exit()
    {
        // TODO : Keep fx back to the pool instead
        GameObject.Destroy(_currentFx);
    }
}

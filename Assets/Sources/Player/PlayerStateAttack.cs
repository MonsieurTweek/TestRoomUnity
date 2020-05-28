using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStateAttack : CharacterFSM.State
{
    private const string ANIMATION_PARAM = "Attack_1H";

    public GameObject fx = null;
    public Transform anchor = null;

    private List<GameObject> _fxInPool = new List<GameObject>();
    private List<GameObject> _fxInUse = new List<GameObject>();

    public override void Enter()
    {
        ((PlayerFSM)owner).animator.SetTrigger(ANIMATION_PARAM);
    }

    public void OnAttackPlayFx()
    {
        if (fx != null && anchor != null)
        {
            if (_fxInUse.Count >= _fxInPool.Count)
            {
                GameObject newFx = GameObject.Instantiate<GameObject>(fx);

                newFx.transform.position = anchor.position;
                newFx.transform.rotation = owner.transform.rotation;

                _fxInUse.Add(newFx);
            }
            else
            {
                // Use from pool
            }
        }
    }
}

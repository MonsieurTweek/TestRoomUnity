using System;
using UnityEngine;

/// <summary>
/// Define what to do when enemy dies
/// </summary>
[Serializable]
public class EnemyStateDie : CharacterFSM.State
{
    public GameObject fx = null;

    private const string ANIMATION_PARAM = "Die";

    public override void Enter()
    {
        ((EnemyFSM)owner).animator.SetTrigger(ANIMATION_PARAM);
    }

    public override void Exit()
    {
        if (fx != null)
        {
            GameObject.Instantiate(fx, owner.transform.position, owner.transform.rotation);
        }
    }
}
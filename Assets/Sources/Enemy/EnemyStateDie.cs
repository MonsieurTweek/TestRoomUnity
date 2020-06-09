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
        owner.animator.SetTrigger(ANIMATION_PARAM);
    }

    public override void Exit()
    {
        CharacterGameEvent.instance.DieRaised(owner.data);

        if (fx != null)
        {
            GameObject.Instantiate(fx, owner.transform.position, owner.transform.rotation);
        }
    }
}
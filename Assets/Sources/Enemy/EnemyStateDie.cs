using System;
using UnityEngine;

/// <summary>
/// Define what to do when enemy dies
/// </summary>
[Serializable]
public class EnemyStateDie : CharacterFSM.CharacterState
{
    public GameObject fx = null;

    private const string ANIMATION_PARAM = "Die";

    public override void Enter()
    {
        character.animator.SetTrigger(ANIMATION_PARAM);
    }

    public override void Exit()
    {
        CharacterGameEvent.instance.DieRaised(character.data);

        if (fx != null)
        {
            GameObject.Instantiate(fx, character.transform.position, character.transform.rotation);
        }
    }
}
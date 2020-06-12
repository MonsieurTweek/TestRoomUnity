using System;
using UnityEngine;

[Serializable]
public class EnemyStateMove : EnemyStateReaction
{
    private const string ANIMATION_PARAM = "Move";

    public float movementSpeed = 3f;

    public override void Enter(Action reaction)
    {
        base.Enter(reaction);

        character.animator.applyRootMotion = true;

        character.animator.SetBool(ANIMATION_PARAM, true);
    }

    public override void Exit()
    {
        character.animator.SetBool(ANIMATION_PARAM, false);
    }

    public override void FixedUpdate()
    {
        Vector3 targetPosition = character.transform.position + Vector3.forward * movementSpeed;

        character.transform.Translate((targetPosition - character.transform.position) * Time.deltaTime);
    }
}
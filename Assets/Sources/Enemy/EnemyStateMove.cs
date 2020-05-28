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

        ((EnemyFSM)owner).animator.applyRootMotion = true;

        ((EnemyFSM)owner).animator.SetBool(ANIMATION_PARAM, true);
    }

    public override void Exit()
    {
        ((EnemyFSM)owner).animator.SetBool(ANIMATION_PARAM, false);
    }

    public override void FixedUpdate()
    {
        Vector3 targetPosition = owner.transform.position + Vector3.forward * movementSpeed;

        owner.transform.Translate((targetPosition - owner.transform.position) * Time.deltaTime);
    }
}
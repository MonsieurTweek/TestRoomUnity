using System;
using UnityEngine;

[Serializable]
public class EnemyStateMove : EnemyStateReaction
{
    private const string ANIMATION_PARAM = "Move";

    public Rigidbody body = null;
    public float movementSpeed = 3f;

    public override void Enter(Action reaction)
    {
        base.Enter(reaction);

        body.isKinematic = false;
        ((EnemyFSM)owner).animator.applyRootMotion = true;

        ((EnemyFSM)owner).animator.SetBool(ANIMATION_PARAM, true);
    }

    public override void Exit()
    {
        ((EnemyFSM)owner).animator.SetBool(ANIMATION_PARAM, false);
    }

    public override void FixedUpdate()
    {
        body.MovePosition(owner.transform.position + (owner.transform.forward * movementSpeed * Time.deltaTime));
    }
}
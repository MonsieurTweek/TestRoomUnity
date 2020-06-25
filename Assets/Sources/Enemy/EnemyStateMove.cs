using System;
using UnityEngine;

[Serializable]
public class EnemyStateMove : EnemyStateReaction
{
    private const string ANIMATION_PARAM = "Move";

    public float movementSpeed = 3f;

    public override void Enter()
    {
        base.Enter();

        character.animator.applyRootMotion = true;

        character.animator.SetBool(ANIMATION_PARAM, true);
    }

    public override void Exit()
    {
        character.animator.SetBool(ANIMATION_PARAM, false);
    }

    public override void FixedUpdate()
    {
        float speed = character.data.HasStatus(CharacterStatusEnum.FREEZE) ? movementSpeed * 0.5f : movementSpeed;

        Vector3 targetPosition = character.transform.position + Vector3.forward * speed;

        character.transform.Translate((targetPosition - character.transform.position) * Time.deltaTime);
    }
}
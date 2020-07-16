using System;
using UnityEngine;

[Serializable]
public class EnemyStateMove : EnemyStateReaction
{
    private const string ANIMATION_PARAM_MOVE = "Move";
    private const string ANIMATION_PARAM_SPEED = "Speed";

    public bool isRunning = false;
    public float speed = 3f;

    public override void Enter()
    {
        base.Enter();

        character.animator.applyRootMotion = true;

        character.animator.SetBool(ANIMATION_PARAM_MOVE, true);
    }

    public override void Update()
    {
        base.Update();

        float speedTreshold = isRunning == true ? 1f : 0f;

        character.animator.SetFloat(ANIMATION_PARAM_SPEED, speedTreshold);
    }

    public override void FixedUpdate()
    {
        float speedToApply = character.data.HasStatus(CharacterStatusEnum.FREEZE) ? speed * 0.5f : speed;

        Vector3 targetPosition = character.transform.position + Vector3.forward * speedToApply;

        character.transform.Translate((targetPosition - character.transform.position) * Time.deltaTime);
    }

    public override void Exit()
    {
        character.animator.SetBool(ANIMATION_PARAM_MOVE, false);
    }
}
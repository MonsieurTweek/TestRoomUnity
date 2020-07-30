using System;
using UnityEngine;

[Serializable]
public class EnemyStateMove : EnemyStateReaction
{
    private const string ANIMATION_PARAM_MOVE = "Move";
    private const string ANIMATION_PARAM_SPEED = "Speed";
    private const string ANIMATION_PARAM_TURN = "Turn";
    private const string ANIMATION_PARAM_ANGLE = "Angle";

    public bool isRunning = false;
    public float speed = 3f;
    public float rotationSpeed = 1f;

    private bool _isMoving = false;

    public override void Enter()
    {
        base.Enter();

        EvaluateMove();
    }

    /// <summary>
    /// Check whether we turn or move based on angle amplitude to align with direction
    /// </summary>
    private void EvaluateMove()
    {
        float angle = Mathf.Atan2(((EnemyFSM)character).direction.x, ((EnemyFSM)character).direction.z) * Mathf.Rad2Deg;

        character.animator.applyRootMotion = true;

        if (Mathf.Abs(angle) > 35f)
        {
            _isMoving = false;

            character.animator.SetTrigger(ANIMATION_PARAM_TURN);
            character.animator.SetFloat(ANIMATION_PARAM_ANGLE, angle > 0 ? 1f : 0f);
        }
        else
        {
            _isMoving = true;
        }

        character.animator.SetBool(ANIMATION_PARAM_MOVE, _isMoving);
    }

    public override void OnSingleAnimationEnded()
    {
        EvaluateMove();
    }

    public override void Update()
    {
        ((EnemyFSM)character).LookAtTarget(rotationSpeed);

        if (_isMoving == true)
        {
            // Update reaction state only if not lock by turn animation
            base.Update();

            float speedTreshold = isRunning == true ? 1f : 0f;

            character.animator.SetFloat(ANIMATION_PARAM_SPEED, speedTreshold);
        }
    }

    public override void FixedUpdate()
    {
        if (_isMoving == true)
        {
            float speedToApply = character.data.HasStatus(CharacterStatusEnum.FREEZE) ? speed * 0.5f : speed;

            Vector3 targetPosition = character.transform.position + Vector3.forward * speedToApply;

            character.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = speedToApply;
            character.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(((EnemyFSM)character).target.transform.position);
        }
    }

    public override void Exit()
    {
        _isMoving = false;

        character.animator.SetBool(ANIMATION_PARAM_MOVE, false);
        character.animator.SetBool(ANIMATION_PARAM_TURN, false);

        character.animator.applyRootMotion = false;

        character.GetComponent<UnityEngine.AI.NavMeshAgent>().ResetPath();
    }
}
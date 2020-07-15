using System;
using UnityEngine;

[Serializable]
public class EnemyStateMove : EnemyStateReaction
{
    private const string ANIMATION_PARAM_MOVE = "Move";
    private const string ANIMATION_PARAM_SPEED = "Speed";

    public bool isRunning = false;
    public float speed = 3f;
    public float timeBeforeDash = 2f;
    public float minimumDashDistance = 10f;

    private float _inRangeTime = 0f;
    private float _outOfRangeTime = 0f;

    public override void Enter()
    {
        base.Enter();

        character.animator.applyRootMotion = true;

        character.animator.SetBool(ANIMATION_PARAM_MOVE, true);

        // Initialize time values to start with the change of state
        _inRangeTime = Time.time;
        _outOfRangeTime = Time.time;
    }

    public override void Update()
    {
        base.Update();

        // Target is too far, too long
        if (_isInRange == false && 
            _sqrDistanceToTarget > minimumDashDistance * minimumDashDistance &&
            Time.time - _outOfRangeTime >= timeBeforeDash)
        {
            ((EnemyFSM)character).TransitionToDash();
        }

        float speedTreshold = isRunning == true ? 1f : 0f;

        character.animator.SetFloat(ANIMATION_PARAM_SPEED, speedTreshold);
    }

    public override void FixedUpdate()
    {
        float speedToApply = character.data.HasStatus(CharacterStatusEnum.FREEZE) ? speed * 0.5f : speed;

        Vector3 targetPosition = character.transform.position + Vector3.forward * speedToApply;

        character.transform.Translate((targetPosition - character.transform.position) * Time.deltaTime);
    }

    public override void EnterRange()
    {
        base.EnterRange();

        _inRangeTime = Time.time;
        _outOfRangeTime = float.MaxValue;
    }

    public override void ExitRange()
    {
        base.ExitRange();

        _inRangeTime = float.MaxValue;
        _outOfRangeTime = Time.time;
    }

    public override void Exit()
    {
        character.animator.SetBool(ANIMATION_PARAM_MOVE, false);
    }

#if UNITY_EDITOR
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        UnityEditor.Handles.color = UnityEngine.Color.blue;
        UnityEditor.Handles.DrawWireDisc(owner.transform.position, UnityEngine.Vector3.up, minimumDashDistance * 2f);
    }
#endif
}
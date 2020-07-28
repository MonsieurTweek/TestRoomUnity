using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Trigger a reaction when the target enters a range
/// </summary>
[Serializable]
public class EnemyStateReaction : CharacterFSM.CharacterState
{
    public float range = 1f;
    public float rangedActionDelay = 2f;
    public float rangedActionDistance = 10f;

    public UnityEvent onEnterRange = null;
    public UnityEvent onExitRange = null;
    public UnityEvent onStayOutOfRange = null;

    protected bool _isInRange = false;
    protected float _sqrDistanceToTarget = 0f;

    private float _inRangeTime = 0f;
    private float _outOfRangeTime = 0f;

    public override void Enter()
    {
        _isInRange = false;

        // Initialize time values to start with the change of state
        _inRangeTime = Time.time;
        _outOfRangeTime = Time.time;
    }

    public override void Update()
    {
        _sqrDistanceToTarget = ((EnemyFSM)character).direction.sqrMagnitude;

        // Enter range
        if (_isInRange == false && _sqrDistanceToTarget < range * range)
        {
            EnterRange();
        }
        // Leave range
        else if (_isInRange == true && _sqrDistanceToTarget > range * range)
        {
            ExitRange();
        }

        // Target is too far, too long
        if (onStayOutOfRange != null && _isInRange == false &&
            _sqrDistanceToTarget > rangedActionDistance * rangedActionDistance &&
            Time.time - _outOfRangeTime >= rangedActionDelay)
        {
            onStayOutOfRange.Invoke();
        }
    }

    public virtual void EnterRange()
    {
        _isInRange = true;
        _inRangeTime = Time.time;
        _outOfRangeTime = float.MaxValue;

        onEnterRange.Invoke();
    }

    public virtual void ExitRange()
    {
        _isInRange = false;
        _inRangeTime = float.MaxValue;
        _outOfRangeTime = Time.time;

        onExitRange.Invoke();
    }

    public override void Exit()
    {
        // Clean range value to be sure we don't have artifact when entering the state later
        _isInRange = false;
    }

#if UNITY_EDITOR
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        UnityEditor.Handles.color = _isInRange == true ? UnityEngine.Color.green : UnityEngine.Color.red;
        UnityEditor.Handles.DrawWireDisc(owner.transform.position, UnityEngine.Vector3.up, range * 2f);

        UnityEditor.Handles.color = UnityEngine.Color.blue;
        UnityEditor.Handles.DrawWireDisc(owner.transform.position, UnityEngine.Vector3.up, rangedActionDistance * 2f);
    }
#endif
}
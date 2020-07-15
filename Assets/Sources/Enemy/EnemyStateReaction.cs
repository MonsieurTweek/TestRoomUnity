using System;
using UnityEngine.Events;

/// <summary>
/// Trigger a reaction when the target enters a range
/// </summary>
[Serializable]
public class EnemyStateReaction : CharacterFSM.CharacterState
{
    public float range = 1f;

    public UnityEvent onEnterRange = null;
    public UnityEvent onExitRange = null;

    protected bool _isInRange = false;
    protected float _sqrDistanceToTarget = 0f;

    public override void Update()
    {
        ((EnemyFSM)character).LookAtTarget();

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
    }

    public virtual void EnterRange()
    {
        _isInRange = true;

        onEnterRange.Invoke();
    }

    public virtual void ExitRange()
    {
        _isInRange = false;

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
        UnityEditor.Handles.DrawWireDisc(owner.transform.position, UnityEngine.Vector3.up, range * range);
    }
#endif
}
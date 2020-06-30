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

    public override void Update()
    {
        ((EnemyFSM)character).LookAtTarget();

        float sqrDistance = ((EnemyFSM)character).direction.sqrMagnitude;

        // Enter range
        if (_isInRange == false && sqrDistance < range * range)
        {
            onEnterRange.Invoke();
        }
        // Leave range
        else if (_isInRange == true && sqrDistance > range * range)
        {
            onExitRange.Invoke();
        }
    }

#if UNITY_EDITOR
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        UnityEditor.Handles.color = UnityEngine.Color.green;
        UnityEditor.Handles.DrawWireDisc(owner.transform.position, UnityEngine.Vector3.up, range * range);
    }
#endif
}
using System;
using UnityEngine.Events;

/// <summary>
/// Trigger a reaction when the target enters a range
/// </summary>
[Serializable]
public class EnemyStateReaction : CharacterFSM.CharacterState
{
    public float closeRange = 1f;
    public float farRange = 1f;

    public UnityEvent onCloseRange = null;
    public UnityEvent onFarRange = null;

    public override void Update()
    {
        ((EnemyFSM)character).LookAtTarget();

        float sqrDistance = ((EnemyFSM)character).direction.sqrMagnitude;

        if (sqrDistance < closeRange * closeRange)
        {
            onCloseRange.Invoke();
        }
        else if (sqrDistance < farRange * farRange)
        {
            onFarRange.Invoke();
        }
    }

#if UNITY_EDITOR
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        UnityEditor.Handles.color = UnityEngine.Color.green;
        UnityEditor.Handles.DrawWireDisc(owner.transform.position, UnityEngine.Vector3.up, closeRange * closeRange);

        UnityEditor.Handles.color = UnityEngine.Color.red;
        UnityEditor.Handles.DrawWireDisc(owner.transform.position, UnityEngine.Vector3.up, farRange * farRange);
    }
#endif
}
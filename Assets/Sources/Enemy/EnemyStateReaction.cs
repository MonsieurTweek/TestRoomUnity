using System;

/// <summary>
/// Trigger a reaction when the target enters a range
/// </summary>
[Serializable]
public class EnemyStateReaction : CharacterFSM.CharacterState1Param<Action>
{
    public float reactionRange = 1f;

    protected Action _reaction = null;

    public override void Enter(Action reaction)
    {
        _reaction = reaction;
    }

    public override void Update()
    {
        ((EnemyFSM)character).LookAtTarget();

        if (((EnemyFSM)character).direction.sqrMagnitude < reactionRange * reactionRange)
        {
            _reaction();
        }
    }
}
using System;

/// <summary>
/// Trigger a reaction when the target enters a range
/// </summary>
[Serializable]
public class EnemyStateReaction : FSM.State1Param<Action>
{
    public float reactionRange = 1f;

    internal Action reaction = null;

    public override void Enter(Action reaction)
    {
        this.reaction = reaction;
    }

    public override void Update()
    {
        ((EnemyFSM)owner).LookAtTarget();

        if (((EnemyFSM)owner).direction.sqrMagnitude < reactionRange * reactionRange)
        {
            reaction();
        }
    }
}
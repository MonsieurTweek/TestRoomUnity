using System;

/// <summary>
/// Defines what to do when enemy attacks
/// </summary>
[Serializable]
public class EnemyStateAttack : CharacterStateAttack
{
    public override void Enter(bool isHeavy)
    {
        owner.animator.applyRootMotion = true;

        base.Enter(isHeavy);
    }

    public override void Exit()
    {
        owner.animator.applyRootMotion = false;
    }
}
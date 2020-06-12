using System;

/// <summary>
/// Defines what to do when enemy attacks
/// </summary>
[Serializable]
public class EnemyStateAttack : CharacterStateAttack
{
    public override void Enter(bool isHeavy)
    {
        character.animator.applyRootMotion = true;

        base.Enter(isHeavy);
    }

    public override void Exit()
    {
        character.animator.applyRootMotion = false;
    }
}
using System;

/// <summary>
/// Defines what to do when enemy is hit
/// </summary>
[Serializable]
public class EnemyStateHit : CharacterFSM.State
{
    private const string ANIMATION_PARAM = "Hit";

    public override void Enter()
    {
        ((EnemyFSM)owner).animator.SetTrigger(ANIMATION_PARAM);
    }
}
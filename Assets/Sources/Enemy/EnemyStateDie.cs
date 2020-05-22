using System;

/// <summary>
/// Define what to do when enemy dies
/// </summary>
[Serializable]
public class EnemyStateDie : FSM.State
{
    private const string ANIMATION_PARAM = "Die";

    public override void Enter()
    {
        ((EnemyFSM)owner).animator.SetTrigger(ANIMATION_PARAM);
    }
}
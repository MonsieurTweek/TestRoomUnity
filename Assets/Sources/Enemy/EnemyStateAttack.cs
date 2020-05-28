using System;

/// <summary>
/// Defines what to do when enemy attacks
/// </summary>
[Serializable]
public class EnemyStateAttack : CharacterFSM.State
{
    private const string ANIMATION_PARAM = "Attack";
    private const string ANIMATION_PARAM_ALT = "Attack_Alt";

    public override void Enter()
    {
        //body.isKinematic = true;
        ((EnemyFSM)owner).animator.applyRootMotion = true;

        ((EnemyFSM)owner).animator.SetTrigger(UnityEngine.Random.Range(0, 2) == 0 ? ANIMATION_PARAM : ANIMATION_PARAM_ALT);
    }

    public override void Exit()
    {
        //body.isKinematic = false;
        ((EnemyFSM)owner).animator.applyRootMotion = false;
    }
}
using System;

/// <summary>
/// Define what to do when player dies
/// </summary>
[Serializable]
public class PlayerStateDie : CharacterFSM.State
{
    private const string ANIMATION_PARAM = "Die";

    public override void Enter()
    {
        ((PlayerFSM)owner).animator.SetTrigger(ANIMATION_PARAM);
    }
}
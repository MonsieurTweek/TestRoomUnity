using System;

/// <summary>
/// Defines what to do when player is hit
/// </summary>
[Serializable]
public class PlayerStateHit : CharacterFSM.State
{
    private const string ANIMATION_PARAM = "Hit";

    public override void Enter()
    {
        ((PlayerFSM)owner).animator.SetTrigger(ANIMATION_PARAM);
    }
}
using System;

/// <summary>
/// Defines what to do when player is hit
/// </summary>
[Serializable]
public class PlayerStateHit : CharacterFSM.CharacterState
{
    private const string ANIMATION_PARAM = "Hit";

    public override void Enter()
    {
        character.animator.SetTrigger(ANIMATION_PARAM);
    }
}
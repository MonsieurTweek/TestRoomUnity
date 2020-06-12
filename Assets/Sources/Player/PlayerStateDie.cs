using System;

/// <summary>
/// Define what to do when player dies
/// </summary>
[Serializable]
public class PlayerStateDie : CharacterFSM.CharacterState
{
    private const string ANIMATION_PARAM = "Die";

    public override void Enter()
    {
        character.animator.SetTrigger(ANIMATION_PARAM);
    }

    public override void Exit()
    {
        CharacterGameEvent.instance.DieRaised(character.data);
    }
}
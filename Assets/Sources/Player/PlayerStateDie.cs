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
        owner.animator.SetTrigger(ANIMATION_PARAM);
    }

    public override void Exit()
    {
        CharacterGameEvent.instance.DieRaised(owner.data);
    }
}
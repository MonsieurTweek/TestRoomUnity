using System;

/// <summary>
/// Defines what to do while introducing enemy
/// </summary>
[Serializable]
public class EnemyStateIntro : CharacterFSM.CharacterState
{
    public override void Enter()
    {
        CharacterGameEvent.instance.IntroStart(owner.transform, character.data);
    }

    public override void Exit()
    {
        CharacterGameEvent.instance.IntroEnd();
    }
}
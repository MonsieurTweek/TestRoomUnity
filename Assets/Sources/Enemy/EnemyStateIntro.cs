using System;

/// <summary>
/// Defines what to do while introducing enemy
/// </summary>
[Serializable]
public class EnemyStateIntro : CharacterFSM.CharacterState
{
    private const string ANIMATION_PARAM = "Unsheathe";

    public bool hasCinematic = true;
    public bool hasUnsheatheTrigger = false;

    public override void Enter()
    {
        if (hasUnsheatheTrigger == true)
        {
            character.animator.SetTrigger(ANIMATION_PARAM);
        }

        if (hasCinematic == true)
        {
            CharacterGameEvent.instance.IntroStart(owner.transform, character.data);
        }
    }

    public override void Exit()
    {
        if (hasCinematic == true)
        {
            CharacterGameEvent.instance.IntroEnd();
        }
    }

    public override void OnSingleAnimationEnded()
    {
        // If no unsheathe animation go directly to idle
        if (hasUnsheatheTrigger == false)
        {
            character.TransitionToIdle();
        }

        // Otherwise wait for next call to transition
        hasUnsheatheTrigger = false;
    }
}
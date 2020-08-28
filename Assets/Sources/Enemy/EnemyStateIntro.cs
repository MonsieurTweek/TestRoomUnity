using System;
using UnityEngine;

/// <summary>
/// Defines what to do while introducing enemy
/// </summary>
[Serializable]
public class EnemyStateIntro : CharacterFSM.CharacterState
{
    private const string ANIMATION_PARAM_CINEMATIC_LOOP = "Cinematic";
    private const string ANIMATION_PARAM_UNSHEATHE = "Unsheathe";

    public bool hasCinematic = true;
    public bool hasUnsheatheTrigger = false;

    public override void Enter()
    {
        character.animator.SetBool(ANIMATION_PARAM_CINEMATIC_LOOP, hasCinematic);

        if (hasUnsheatheTrigger == true)
        {
            character.animator.SetTrigger(ANIMATION_PARAM_UNSHEATHE);
        }

        if (hasCinematic == true)
        {
            CharacterGameEvent.instance.IntroStart(owner.transform, character.data);
        }

        CharacterGameEvent.instance.onIntroEnded += OnIntroEnded;
    }

    public override void OnSingleAnimationEnded()
    {
        // No cinematic/unsheathe triggers transition to idle state
        if (hasCinematic == false && hasUnsheatheTrigger == false)
        {
            character.TransitionToIdle();
        }
        else if (hasUnsheatheTrigger == true)
        {
            // Otherwise wait for next call to transition
            hasUnsheatheTrigger = false;

            // Eventually, pause the intro waiting for player's input
            CharacterGameEvent.instance.IntroPause();
        }
        else
        {
        }
    }

    private void OnIntroEnded()
    {
        character.TransitionToIdle();
    }

    public override void Exit()
    {
        character.animator.SetBool(ANIMATION_PARAM_CINEMATIC_LOOP, false);

        if (CharacterGameEvent.instance != null)
        {
            CharacterGameEvent.instance.onIntroEnded -= OnIntroEnded;
        }
    }
}
using System;
using UnityEngine;

/// <summary>
/// Defines what to do when enemy attacks with a dash towards the player
/// </summary>
[Serializable]
public class EnemyStateDash : CharacterFSM.CharacterState
{
    private const string ANIMATION_NAME_SUFFIX = "_Dash_";
    private const string ANIMATION_PARAM_DASH = "Dash";

    public float offset = 0f;

    private AnimationClip _animation = null;
    private float _delay = 0.2f;
    private float _duration = 1f;
    private Vector3 _destination = Vector3.zero;

    public override void Enter()
    {
        if (_animation == null)
        {
            AnimationClip[] animationClips = character.animator.runtimeAnimatorController.animationClips;

            for (int i = 0; i < animationClips.Length; i++)
            {
                if (animationClips[i].name.Contains(ANIMATION_NAME_SUFFIX))
                {
                    _animation = animationClips[i];
                }
            }
        }

        bool hasStartTime = false;
        float startTime = 0f;
        float stopTime = 0f;
        AnimationEvent[] events = _animation.events;

        for (int i = 0; i < events.Length; i++)
        {
            if(events[i].functionName == CharacterFSM.ANIM_EVENT_MANUAL)
            {
                // Get the start time first
                if (hasStartTime == false)
                {
                    startTime = events[i].time;
                    hasStartTime = true;
                }
                else
                {
                    stopTime = events[i].time;
                }
            }
        }

        _delay = startTime;
        _duration = stopTime - _delay;

        _destination = ((EnemyFSM)character).target.position - character.transform.forward * offset;

        character.animator.SetTrigger(ANIMATION_PARAM_DASH);
        character.animator.applyRootMotion = true;

        LeanTween.move(character.gameObject, _destination, _duration).setDelay(_delay);
    }

    public override void Exit()
    {
        character.animator.applyRootMotion = false;
    }
}
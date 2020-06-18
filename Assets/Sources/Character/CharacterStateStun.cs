using System;
using UnityEngine;

/// <summary>
/// Put character in a stun state
/// </summary>
[Serializable]
public class CharacterStateStun : CharacterFSM.CharacterState2Params<float, Action>
{
    private const string ANIMATION_PARAM = "Stun";

    private float _time = 0f;
    private float _duration = 0f;
    private Action _callback = null;

    public override void Enter(float duration, Action callback)
    {
        _time = Time.time;
        _duration = duration;
        _callback = callback;
        
        character.animator.SetBool(ANIMATION_PARAM, true);
    }

    public override void Exit()
    {
        character.animator.SetBool(ANIMATION_PARAM, false);
    }
}
using System;
using UnityEngine;

/// <summary>
/// Put character in a stun state
/// </summary>
[Serializable]
public class CharacterStateStun : CharacterFSM.CharacterState2Params<float, Action>
{
    private const string ANIMATION_PARAM = "Stun";

    public CharacterOutlineController outline = null;

    private float _time = 0f;
    private float _duration = 0f;
    private Action _callback = null;

    public override void Enter(float duration, Action callback)
    {
        _time = Time.time;
        _duration = duration;
        _callback = callback;
        
        character.animator.SetBool(ANIMATION_PARAM, true);

        outline.Show();
    }

    public override void Exit()
    {
        character.animator.SetBool(ANIMATION_PARAM, false);

        outline.Hide();
    }

    public override void OnSingleAnimationEnded()
    {
        // Do nothing as we can't transition to something else while stun
    }
}
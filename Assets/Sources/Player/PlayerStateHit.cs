using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines what to do when player is hit
/// </summary>
[Serializable]
public class PlayerStateHit : CharacterFSM.CharacterState
{
    private const string ANIMATION_PARAM = "Hit";

    public CharacterOutlineController outline = null;

    public override void Enter()
    {
        character.animator.SetTrigger(ANIMATION_PARAM);

        outline.Show();
    }

    public override void Exit()
    {
        outline.Hide();
    }
}
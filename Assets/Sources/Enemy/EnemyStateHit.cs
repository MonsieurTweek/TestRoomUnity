using System;
using UnityEngine;

/// <summary>
/// Defines what to do when enemy is hit
/// </summary>
[Serializable]
public class EnemyStateHit : CharacterFSM.CharacterState
{
    private const string ANIMATION_PARAM = "Hit";

    public Transform anchor = null;
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
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines what to do when enemy is hit
/// </summary>
[Serializable]
public class EnemyStateHit : CharacterFSM.CharacterState
{
    private const string ANIMATION_PARAM = "Hit";

    public SkinnedMeshRenderer meshRenderer = null;

    private LTDescr _tween = null;

    public override void Enter()
    {
        character.animator.SetTrigger(ANIMATION_PARAM);

        _tween = LeanTween.value(0.03f, 0f, 0.25f).setLoopPingPong(-1).setOnUpdate(OnOutlineUpdate);
    }

    private void OnOutlineUpdate(float width)
    {
        meshRenderer.materials[meshRenderer.materials.Length - 1].SetFloat("_OutlineWidth", width);
    }

    public override void Exit()
    {
        LeanTween.cancel(_tween.id);

        meshRenderer.materials[meshRenderer.materials.Length - 1].SetFloat("_OutlineWidth", 0f);
    }
}
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

    private LTDescr _tween = null;
    public List<SkinnedMeshRenderer> _meshRenderers = new List<SkinnedMeshRenderer>();

    public override void Enter()
    {
        if (_meshRenderers.Count == 0)
        {
            GetSkinnedMeshRenderers();
        }

        character.animator.SetTrigger(ANIMATION_PARAM);

        _tween = LeanTween.value(2f, 0f, 0.25f).setLoopPingPong(-1).setOnUpdate(OnOutlineUpdate);
    }

    private void GetSkinnedMeshRenderers()
    {
        foreach (CustomizationPartOnPlayer part in ((PlayerFSM)character).customizationController.customizableParts)
        {
            foreach (GameObject element in part.elements)
            {
                if (element.activeSelf == true)
                {
                    _meshRenderers.Add(element.GetComponent<SkinnedMeshRenderer>());
                }
            }
        }
    }

    private void OnOutlineUpdate(float width)
    {
        foreach (SkinnedMeshRenderer renderer in _meshRenderers)
        {
            renderer.materials[renderer.materials.Length - 1].SetFloat("_OutlineWidth", width);
        }
    }

    public override void Exit()
    {
        LeanTween.cancel(_tween.id);

        foreach (SkinnedMeshRenderer renderer in _meshRenderers)
        {
            renderer.materials[renderer.materials.Length - 1].SetFloat("_OutlineWidth", 0f);
        }
    }
}
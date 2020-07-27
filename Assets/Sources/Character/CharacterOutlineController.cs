using System.Collections.Generic;
using UnityEngine;

public class CharacterOutlineController : MonoBehaviour
{
    public float width = 0.03f;

    public List<SkinnedMeshRenderer> _meshRenderers = new List<SkinnedMeshRenderer>();

    private int _tweenId = -1;

    public virtual void Show()
    {
        // Add the gameobject to the call so we are sure to cancel the right tween later
        _tweenId = LeanTween.value(gameObject, width, 0f, 0.25f).setLoopPingPong(-1).setOnUpdate(OnOutlineUpdate).id;
    }

    private void OnOutlineUpdate(float width)
    {
        foreach (SkinnedMeshRenderer renderer in _meshRenderers)
        {
            renderer.materials[renderer.materials.Length - 1].SetFloat("_OutlineWidth", width);
        }
    }

    public virtual void Hide()
    {
        if (LeanTween.isTweening(_tweenId))
        {
            LeanTween.cancel(gameObject, _tweenId);
        }

        foreach (SkinnedMeshRenderer renderer in _meshRenderers)
        {
            renderer.materials[renderer.materials.Length - 1].SetFloat("_OutlineWidth", 0f);
        }
    }

    private void OnDestroy()
    {
        if (LeanTween.isTweening(_tweenId))
        {
            LeanTween.cancel(gameObject, _tweenId);
        }
    }
}
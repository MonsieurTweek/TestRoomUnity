using System.Collections.Generic;
using UnityEngine;

public class CharacterOutlineController : MonoBehaviour
{
    public float width = 0.03f;

    public List<SkinnedMeshRenderer> _meshRenderers = new List<SkinnedMeshRenderer>();

    private LTDescr _tween = null;

    public virtual void Show()
    {
        _tween = LeanTween.value(width, 0f, 0.25f).setLoopPingPong(-1).setOnUpdate(OnOutlineUpdate);
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
        LeanTween.cancel(_tween.id);

        foreach (SkinnedMeshRenderer renderer in _meshRenderers)
        {
            renderer.materials[renderer.materials.Length - 1].SetFloat("_OutlineWidth", 0f);
        }
    }
}

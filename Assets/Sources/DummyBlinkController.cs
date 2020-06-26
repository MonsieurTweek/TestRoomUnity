using UnityEngine;

public class DummyBlinkController : MonoBehaviour
{
    public MeshRenderer meshRenderer = null;

    private LTDescr _tween = null;

    private void Start()
    {
        _tween = LeanTween.value(0.05f, 0f, 0.25f).setLoopPingPong(-1).setOnUpdate(OnOutlineUpdate);
    }

    private void OnOutlineUpdate(float width)
    {
        meshRenderer.material.SetFloat("_OutlineWidth", width);
    }
}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileController : Button
{
    public Image icon = null;
    public float iconAnimationTime = 0.25f;
    public LeanTweenType iconAnimationIn = LeanTweenType.linear;
    public LeanTweenType iconAnimationOut = LeanTweenType.linear;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        LeanTween.scale(icon.gameObject, Vector3.one * 1.1f, iconAnimationTime).setEase(iconAnimationIn);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);

        LeanTween.scale(icon.gameObject, Vector3.one, iconAnimationTime).setEase(iconAnimationOut);
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileController : Button
{
    public Image background = null;
    public TextMeshProUGUI title = null;
    public Color normalTitleColor = Color.white;
    public Color selectedTitleColor = Color.white;
    public Image icon = null;
    public float iconAnimationTime = 0.25f;
    public LeanTweenType iconAnimationIn = LeanTweenType.linear;
    public LeanTweenType iconAnimationOut = LeanTweenType.linear;

    public ProgressBarController confirmBar = null;
    public float confirmDelay = 0.25f;

    private int _tweenId = -1;

    public override void OnSelect(BaseEventData eventData)
    {
        if (background != null)
        {
            background.color = colors.selectedColor;
        }

        if (title != null)
        {
            title.color = selectedTitleColor;
        }

        targetGraphic.color = colors.selectedColor;

        LeanTween.scale(this.gameObject, Vector3.one * 1.1f, iconAnimationTime).setEase(iconAnimationIn);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        if (background != null)
        {
            background.color = colors.normalColor;
        }

        if (title != null)
        {
            title.color = normalTitleColor;
        }

        targetGraphic.color = colors.normalColor;

        LeanTween.scale(this.gameObject, Vector3.one, iconAnimationTime).setEase(iconAnimationOut);
    }

    public void ConfirmSelection()
    {
        _tweenId = LeanTween.value(0f, 100f, confirmDelay).setOnUpdate(ConfirmProgress).setOnComplete(ConfirmComplete).id;
    }

    private void ConfirmProgress(float progress)
    {
        confirmBar.current = Mathf.RoundToInt(progress);
    }

    private void ConfirmComplete()
    {
        AudioManager.instance.PlayMenuSound(AudioManager.instance.menuConfirmationSfx);

        onClick.Invoke();

        confirmBar.current = 0;
    }

    public void CancelSelection()
    {
        if (LeanTween.isTweening(_tweenId) == true)
        {
            LeanTween.cancel(_tweenId);

            confirmBar.current = 0;
        }
    }

    public void DisableConfirmation()
    {
        confirmBar.gameObject.SetActive(false);
    }
}
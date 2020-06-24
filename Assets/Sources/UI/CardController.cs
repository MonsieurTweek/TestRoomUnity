using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    public GameObject back = null;
    public GameObject front = null;
    public TextMeshProUGUI titleText = null;
    public TextMeshProUGUI descriptionText = null;
    public Image icon = null;

    private Vector3 _destination = Vector3.zero;

    public CardData data { private set; get; }

    private void Awake()
    {
        data = new CardData();

        // Save current position as destination
        _destination = transform.localPosition;

        Prepare();
    }

    public void Initialize(DescriptiveObject perk)
    {
        data.Populate(perk);

        titleText.text = data.title;
        descriptionText.text = data.description;
        icon.sprite = data.icon;
    }

    private void Prepare()
    {
        // Position is reset to be out of screen
        transform.localPosition = Vector3.left * Screen.width;

        // Scale is reset as well
        transform.localScale = Vector3.one;
        back.transform.localScale = Vector3.one;
        front.transform.localScale = Vector3.one;

        back.SetActive(true);
        front.SetActive(false);
    }

    public void AnimateIntro()
    {
        Prepare();

        LeanTween.moveLocal(gameObject, _destination, 0.5f).setOnComplete(AnimateSwitch);
    }

    public void AnimateSwitch()
    {
        LeanTween.scaleX(back, 0f, 0.25f).setOnComplete(ShowFront);
        LeanTween.scaleX(front, 1f, 0.75f);
    }

    public void AnimateSelection(Action<object> onComplete)
    {
        LeanTween.scale(gameObject, Vector3.one * 1.2f, 0.5f).setOnComplete(onComplete, data.uniqueId);
    }

    public void AnimateHide()
    {
        LeanTween.scale(gameObject, Vector3.zero, 0.5f).setOnComplete(Hide);
    }

    public void OnSelect()
    {
        PerkGameEvent.instance.Select(data);
        PerkGameEvent.instance.Unlock(data);
    }

    private void ShowFront()
    {
        back.SetActive(false);
        front.SetActive(true);
    }

    public void Hide()
    {
        back.SetActive(false);
        front.SetActive(false);
    }
}
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

    public DescriptiveObject perk = null;

    private Vector3 _destination = Vector3.zero;

    public CardData data { private set; get; }

    private void Awake()
    {
        data = new CardData();
        data.Populate(perk);

        Prepare();
    }

    private void Prepare()
    {
        titleText.text = data.title;
        descriptionText.text = data.description;
        icon.sprite = data.icon;

        // Start at destination
        _destination = transform.localPosition;

        // And position is reset to be out of screen
        transform.position = Vector3.left * Screen.width;

        back.SetActive(true);
        front.SetActive(false);
    }

    public void AnimateIntro()
    {
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
        PerkGameEvent.instance.PerkSelectedRaised(data);
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
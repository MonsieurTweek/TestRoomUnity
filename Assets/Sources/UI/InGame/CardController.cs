using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    public Image icon = null;
    public GameObject back = null;
    public GameObject front = null;
    public TextMeshProUGUI titleText = null;
    public TextMeshProUGUI descriptionText = null;

    public CardData data { private set; get; }

    private Vector3 _destination = Vector3.zero;
    private Action _onFullIntroCompleted = null;
    private Canvas _canvas = null;

    private void Awake()
    {
        data = new CardData();

        // Save current position as destination
        _destination = transform.localPosition;

        _canvas = GetComponent<Canvas>();

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

    public void Select()
    {
        _canvas.sortingOrder = 10;
        LeanTween.scale(gameObject, Vector3.one * 1.2f, 0.25f);
    }

    public void Deselect()
    {
        _canvas.sortingOrder = 1;
        LeanTween.scale(gameObject, Vector3.one, 0.25f);
    }

    public void AnimateIntro(Action onComplete)
    {
        _onFullIntroCompleted = onComplete;

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

    public void ConfirmSelection()
    {
        PerkGameEvent.instance.Unlock(data);
    }

    private void ShowFront()
    {
        back.SetActive(false);
        front.SetActive(true);

        _onFullIntroCompleted.Invoke();
    }

    public void Hide()
    {
        back.SetActive(false);
        front.SetActive(false);
    }
}
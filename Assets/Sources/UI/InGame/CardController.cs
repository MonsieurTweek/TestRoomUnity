using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    [Header("Refrences")]
    public Image icon = null;
    public Image iconBackground = null;
    public GameObject back = null;
    public GameObject front = null;
    public TextMeshProUGUI titleText = null;
    public TextMeshProUGUI descriptionText = null;

    public CardData data { private set; get; }

    private Vector3 _destination = Vector3.zero;
    private Vector3 _rotation = Vector3.zero;
    private Action _onIntroSwitch = null;
    private Action _onFullIntroCompleted = null;
    private Canvas _canvas = null;

    private int _tweenId = -1;

    private void Awake()
    {
        data = new CardData();

        // Save current position as destination
        _destination = transform.localPosition;
        _rotation = transform.rotation.eulerAngles;

        _canvas = GetComponent<Canvas>();

        Prepare();
    }

    public void Initialize(DescriptiveObject perk)
    {
        data.Populate(perk);

        titleText.text = data.title;
        descriptionText.text = data.description.Replace(Perk.AMOUNT_STRING_KEY, ((Perk)perk).amount.ToString());
        icon.sprite = data.icon;
        iconBackground.sprite = data.background;
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
        LeanTween.scale(gameObject, Vector3.one * 1.1f, 0.25f);

        // Start a select animation to show which one is selected
        _tweenId = LeanTween.rotate(gameObject, gameObject.transform.rotation.eulerAngles + new Vector3(0f, 0f, 2f), 0.5f).setLoopPingPong(-1).id;
    }

    public void Deselect()
    {
        _canvas.sortingOrder = 1;
        LeanTween.scale(gameObject, Vector3.one, 0.25f);

        // Reset select animation
        LeanTween.cancel(_tweenId);
        gameObject.transform.rotation = Quaternion.Euler(_rotation);
    }

    public void AnimateIntro(Action onSwitch, Action onComplete)
    {
        _onIntroSwitch = onSwitch;
        _onFullIntroCompleted = onComplete;

        Prepare();

        LeanTween.moveLocal(gameObject, _destination, 0.5f).setOnComplete(AnimateSwitch);
    }

    public void AnimateSwitch()
    {
        LeanTween.scaleX(back, 0f, 0.25f).setOnComplete(ShowFront);
        LeanTween.scaleX(front, 1f, 0.75f);

        _onIntroSwitch.Invoke();
    }

    public void AnimateUnlock(Action<object> onComplete)
    {
        LeanTween.scale(gameObject, Vector3.one * 1.2f, 0.5f).setOnComplete(onComplete, data.uniqueId);
    }

    public void AnimateHide()
    {
        LeanTween.scale(gameObject, Vector3.zero, 0.5f).setOnComplete(Hide);
    }

    public void ConfirmSelection()
    {
        PerkGameEvent.instance.StartUnlock(data);
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
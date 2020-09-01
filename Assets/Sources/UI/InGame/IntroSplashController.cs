using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class IntroSplashController : MonoBehaviour
{
    [Header("References")]
    public RectTransform layout = null;
    public Image flash = null; 
    public TextMeshProUGUI title = null;
    public ProgressBarController confirmBar = null;

    [Header("Properties")]
    public LeanTweenType animationType = LeanTweenType.linear;
    public float animationDuration = 0.5f;
    public Vector3 positionOffset = Vector3.zero;
    public float confirmDelay = 0.25f;

    private int _tweenId = -1;
    private Transform _target = null;
    private Vector3 _originalPosition = Vector3.zero;

    private void Awake()
    {
        _originalPosition = layout.transform.position;
    }

    private void Start()
    {
        CharacterGameEvent.instance.onIntroStarted += OnIntroStarted;
        CharacterGameEvent.instance.onIntroPaused += OnIntroPaused;
        CharacterGameEvent.instance.onIntroEnded += OnIntroEnded;
    }

    private void OnIntroStarted(Transform target, AbstractCharacterData data)
    {
        title.text = data.name;
        confirmBar.current = 0;

        _target = target;
    }

    private void OnIntroPaused()
    {
        flash.gameObject.SetActive(true);
        LeanTween.alpha(flash.rectTransform, 1f, 0.15f).setLoopPingPong(1).setEase(LeanTweenType.easeInBack);

        Vector3 finalPosition = _target.position 
            // Up it to the target height
            + _target.up * (_target.localScale.y + positionOffset.y)
            // Offset it to be on the left side of the target
            - _target.right * (_target.localScale.x + positionOffset.x)
            // Put it a little bit behind the target
            - _target.forward * (positionOffset.z);

        // Position is reset to be behind camera
        layout.position = Camera.main.transform.position - Camera.main.transform.forward;

        layout.localRotation = _target.rotation;
        layout.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _target.localScale.x * 2f);

        LeanTween.move(layout.gameObject, finalPosition, animationDuration).setEase(animationType).setDelay(0.1f);

        layout.gameObject.SetActive(true);

        InputManager.instance.menu.Confirm.started += OnConfirmStarted;
        InputManager.instance.menu.Confirm.canceled += OnConfirmCanceled;
    }

    private void OnIntroEnded()
    {
        flash.gameObject.SetActive(false);
        layout.gameObject.SetActive(false);
    }

    private void OnConfirmStarted(InputAction.CallbackContext context)
    {
        _tweenId = LeanTween.value(gameObject, 0f, 100f, confirmDelay).setOnUpdate(ConfirmProgress).setOnComplete(ConfirmComplete).id;
    }

    private void ConfirmProgress(float progress)
    {
        confirmBar.current = Mathf.RoundToInt(progress);
    }

    private void ConfirmComplete()
    {
        CharacterGameEvent.instance.IntroEnd();

        InputManager.instance.menu.Confirm.started -= OnConfirmStarted;
        InputManager.instance.menu.Confirm.canceled -= OnConfirmCanceled;
    }

    private void OnConfirmCanceled(InputAction.CallbackContext context)
    {
        if (LeanTween.isTweening(_tweenId) == true)
        {
            LeanTween.cancel(gameObject, _tweenId);

            confirmBar.current = 0;
        }
    }

    private void OnDestroy()
    {
        if (CharacterGameEvent.instance != null)
        {
            CharacterGameEvent.instance.onIntroStarted -= OnIntroStarted;
            CharacterGameEvent.instance.onIntroPaused -= OnIntroPaused;
            CharacterGameEvent.instance.onIntroEnded -= OnIntroEnded;
        }
    }
}
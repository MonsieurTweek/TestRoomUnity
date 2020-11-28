using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class HomeButtonController : MonoBehaviour
{
    private static readonly string ANALYTICS_UI_ELEMENT = "back";

    [Header("References")]
    public Image icon = null;
    public ProgressBarController confirmBar = null;

    [Header("Properties")]
    public float confirmDelay = 0.25f;
    public bool useConfirmButton = true;

    private int _tweenId = -1;
    private Button _button = null;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        if (useConfirmButton == true)
        {
            InputManager.instance.menu.Confirm.started += OnBackStarted;
            InputManager.instance.menu.Confirm.canceled += OnBackCanceled;
        }
        else
        {
            InputManager.instance.menu.Cancel.started += OnBackStarted;
            InputManager.instance.menu.Cancel.canceled += OnBackCanceled;
        }
    }

    private void OnDisable()
    {
        if (useConfirmButton == true)
        {
            InputManager.instance.menu.Confirm.started -= OnBackStarted;
            InputManager.instance.menu.Confirm.canceled -= OnBackCanceled;
        }
        else
        {
            InputManager.instance.menu.Cancel.started -= OnBackStarted;
            InputManager.instance.menu.Cancel.canceled -= OnBackCanceled;
        }
    }

    private void OnBackStarted(InputAction.CallbackContext context)
    {
        _tweenId = LeanTween.value(0f, 100f, confirmDelay).setOnUpdate(ConfirmProgress).setOnComplete(ConfirmComplete).id;

        LeanTween.scale(icon.gameObject, icon.transform.localScale * 1.5f, 0.1f).setLoopPingPong(1);

        AnalyticsManager.StartConfirmInput(ANALYTICS_UI_ELEMENT);
    }

    private void ConfirmProgress(float progress)
    {
        confirmBar.current = Mathf.RoundToInt(progress);
    }

    private void ConfirmComplete()
    {
        AudioManager.instance.PlayMenuSound(AudioManager.instance.menuBackSfx);

        _button.onClick.Invoke();
    }

    private void OnBackCanceled(InputAction.CallbackContext context)
    {
        if (LeanTween.isTweening(_tweenId) == true)
        {
            AnalyticsManager.CancelConfirmInput(ANALYTICS_UI_ELEMENT, confirmBar.current);

            LeanTween.cancel(_tweenId);

            confirmBar.current = 0;
        }
    }
}

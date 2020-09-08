using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class HomeButtonController : MonoBehaviour
{
    [Header("References")]
    public Image icon = null;
    public ProgressBarController confirmBar = null;

    [Header("Properties")]
    public float confirmDelay = 0.25f;

    private int _tweenId = -1;
    private Button _button = null;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        InputManager.instance.menu.Cancel.started += OnBackStarted;
        InputManager.instance.menu.Cancel.canceled += OnBackCanceled;
    }

    private void OnDisable()
    {
        InputManager.instance.menu.Cancel.started -= OnBackStarted;
        InputManager.instance.menu.Cancel.canceled -= OnBackCanceled;
    }

    private void OnBackStarted(InputAction.CallbackContext context)
    {
        _tweenId = LeanTween.value(0f, 100f, confirmDelay).setOnUpdate(ConfirmProgress).setOnComplete(ConfirmComplete).id;

        LeanTween.scale(icon.gameObject, icon.transform.localScale * 1.5f, 0.1f).setLoopPingPong(1);
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
            LeanTween.cancel(_tweenId);

            confirmBar.current = 0;
        }
    }
}

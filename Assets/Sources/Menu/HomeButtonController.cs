using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class HomeButtonController : MonoBehaviour
{
    public ProgressBarController confirmBar = null;
    public float confirmDelay = 0.25f;

    private LTDescr _confirmAnimation = null;
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
        _confirmAnimation = LeanTween.value(0f, 100f, confirmDelay).setOnUpdate(ConfirmProgress).setOnComplete(ConfirmComplete);
    }

    private void ConfirmProgress(float progress)
    {
        confirmBar.current = Mathf.RoundToInt(progress);
    }

    private void ConfirmComplete()
    {
        _button.onClick.Invoke();
    }

    private void OnBackCanceled(InputAction.CallbackContext context)
    {
        if (_confirmAnimation != null)
        {
            LeanTween.cancel(_confirmAnimation.id);

            confirmBar.current = 0;
        }
    }
}

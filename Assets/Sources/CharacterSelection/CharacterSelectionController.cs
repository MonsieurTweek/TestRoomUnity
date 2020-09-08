using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Manage character selection through the scene
/// </summary>
public class CharacterSelectionController : MonoBehaviour
{
    [Header("References")]
    public CinemachineVirtualCamera introCamera = null;
    public CharacterSelectionCanvasController selectionCanvas = null;
    public CharacterDescriptionCanvasController descriptionCanvas = null;
    public ProgressBarController confirmBar = null;

    public CharacterSelectedController[] characters = null;

    [Header("Properties")]
    public int initialIndex = 0;
    public float confirmDelay = 0.25f;

    private int _tweenId = -1;
    private int _currentIndex = 0;

    private void Awake()
    {
        _currentIndex = initialIndex;

        StartCoroutine(WaitForIntro());
    }

    private void OnEnable()
    {
        InputManager.instance.menu.Navigate.performed += OnNavigatePerformed;
        InputManager.instance.menu.Confirm.started += OnConfirmStarted;
        InputManager.instance.menu.Confirm.canceled += OnConfirmCanceled;
    }

    private void OnDisable()
    {
        InputManager.instance.menu.Navigate.performed -= OnNavigatePerformed;
        InputManager.instance.menu.Confirm.started -= OnConfirmStarted;
        InputManager.instance.menu.Confirm.canceled -= OnConfirmCanceled;
    }

    private void OnNavigatePerformed(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        // Move to next character
        if (input.x < 0f && Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            DeselectCharacter(_currentIndex);

            _currentIndex = _currentIndex > 0 ? _currentIndex - 1 : characters.Length - 1;

            SelectCharacter(_currentIndex);
        }
        // Move to previous character
        else if (input.x > 0f && Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            DeselectCharacter(_currentIndex);

            _currentIndex = _currentIndex < characters.Length - 1 ? _currentIndex + 1 : 0;

            SelectCharacter(_currentIndex);
        }
        // Switch character customization
        else if (input.y > 0f)
        {
            characters[_currentIndex].ChangeCustomization();
        }
    }

    private void OnConfirmStarted(InputAction.CallbackContext context)
    {
        if (characters[_currentIndex].Validate() == true)
        {
            _tweenId = LeanTween.value(gameObject, 0f, 100f, confirmDelay).setOnUpdate(ConfirmProgress).setOnComplete(ConfirmComplete).id;

            // Unbind input except Confirm.Canceled so we are sure to don't switch during validation
            InputManager.instance.menu.Navigate.performed -= OnNavigatePerformed;
            InputManager.instance.menu.Confirm.started -= OnConfirmStarted;
        }
    }

    private void ConfirmProgress(float progress)
    {
        confirmBar.current = Mathf.RoundToInt(progress);
    }

    private void ConfirmComplete()
    {
        // Unbind Confirm.Canceled as the validation has been done
        InputManager.instance.menu.Confirm.canceled -= OnConfirmCanceled;

        AudioManager.instance.PlayMenuSound(AudioManager.instance.menuConfirmationSfx);

        GameEvent.instance.CharacterSelected();
    }

    private void OnConfirmCanceled(InputAction.CallbackContext context)
    {
        if (LeanTween.isTweening(_tweenId) == true)
        {
            LeanTween.cancel(gameObject, _tweenId);

            confirmBar.current = 0;
        }

        // Rebind input as player can make another selection/confirmation
        // except Confirm.Canceled still bound
        InputManager.instance.menu.Navigate.performed += OnNavigatePerformed;
        InputManager.instance.menu.Confirm.started += OnConfirmStarted;
    }

    private IEnumerator WaitForIntro()
    {
        yield return new WaitForSeconds(0.2f);

        SelectCharacter(_currentIndex);

        introCamera.m_Priority = 0; 
    }

    private void SelectCharacter(int index)
    {
        AudioManager.instance.PlayMenuSound(AudioManager.instance.menuNavigationSfx);

        characters[index].Select();
        selectionCanvas.UpdateContent(characters[index].title, characters[index].icon, characters[index].isUnlock);
        descriptionCanvas.OnCharacterSelected(characters[index]);
    }

    private void DeselectCharacter(int index)
    {
        characters[index].Deselect();
    }
}
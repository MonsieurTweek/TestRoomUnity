using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Manage character selection through the scene
/// </summary>
public class CharacterSelectionController : MonoBehaviour
{
    [Header("Direct references")]
    public CharacterSelectionCanvasController canvas = null;
    public int nextSceneIndex;

    [Space(10)]
    
    public int initialIndex = 0;
    private int _currentIndex = 0;

    public CinemachineVirtualCamera introCamera = null;
    public CharacterSelectedController[] characters = null;

    private void Awake()
    {
        _currentIndex = initialIndex;

        StartCoroutine(WaitForIntro());
    }

    private void OnEnable()
    {
        InputManager.instance.menu.Navigate.performed += OnNavigate;
        InputManager.instance.menu.Confirm.started += OnConfirmStarted;
    }

    private void OnDisable()
    {
        InputManager.instance.menu.Navigate.performed -= OnNavigate;
        InputManager.instance.menu.Confirm.started -= OnConfirmStarted;
    }

    private void OnNavigate(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        // Move to next character
        if (input.x < 0f && Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            characters[_currentIndex].Deselect();

            _currentIndex = _currentIndex > 0 ? _currentIndex - 1 : characters.Length - 1;

            SelectCharacter(_currentIndex);
        }
        // Move to previous character
        else if (input.x > 0f && Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            characters[_currentIndex].Deselect();

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
            GameEvent.instance.CharacterSelected();
        }
    }

    private IEnumerator WaitForIntro()
    {
        yield return new WaitForSeconds(0.2f);

        SelectCharacter(_currentIndex);

        introCamera.m_Priority = 0; 
    }

    private void SelectCharacter(int index)
    {
        characters[index].Select();
        canvas.UpdateContent(characters[index].title, characters[index].icon, characters[index].isUnlock);
    }
}

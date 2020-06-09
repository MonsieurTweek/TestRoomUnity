using UnityEngine;
using UnityEngine.SceneManagement;

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

    public CharacterSelectedController[] characters = null;

    private void Awake()
    {
        _currentIndex = initialIndex;

        SelectCharacter(_currentIndex);
    }

    private void Update()
    {
        // Move to next character
        if (Input.GetKeyUp(KeyCode.RightArrow) == true)
        {
            characters[_currentIndex].Deselect();

            _currentIndex = _currentIndex < characters.Length - 1? _currentIndex + 1 : 0;

            SelectCharacter(_currentIndex);
        }

        // Move to previous character
        if (Input.GetKeyUp(KeyCode.LeftArrow) == true)
        {
            characters[_currentIndex].Deselect();

            _currentIndex = _currentIndex > 0 ? _currentIndex - 1 : characters.Length - 1;

            SelectCharacter(_currentIndex);
        }

        // Switch character customization
        if (Input.GetKeyUp(KeyCode.UpArrow) == true)
        {
            characters[_currentIndex].ChangeCustomization();
        }

        // Validate character
        if (Input.GetKeyUp(KeyCode.Return) == true)
        {
            characters[_currentIndex].Validate();
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    private void SelectCharacter(int index)
    {
        characters[index].Select();
        canvas.UpdateContent(characters[index].title, characters[index].icon);
    }
}

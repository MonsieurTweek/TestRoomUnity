using UnityEngine;

public class CharacterSelectionController : MonoBehaviour
{
    public CharacterSelectionCanvasController canvas = null;

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
        if (Input.GetKeyUp(KeyCode.RightArrow) == true)
        {
            characters[_currentIndex].Deselect();

            _currentIndex = _currentIndex < characters.Length - 1? _currentIndex + 1 : 0;

            SelectCharacter(_currentIndex);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) == true)
        {
            characters[_currentIndex].Deselect();

            _currentIndex = _currentIndex > 0 ? _currentIndex - 1 : characters.Length - 1;

            SelectCharacter(_currentIndex);
        }
    }

    private void SelectCharacter(int index)
    {
        characters[index].Select();
        canvas.UpdateContent(characters[index].name, characters[index].icon);
    }
}

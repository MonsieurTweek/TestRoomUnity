using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionCanvasController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text = null;

    [SerializeField]
    private Image _image = null;

    public void UpdateContent(string name, Sprite icon)
    {
        _text.text = name;
        _image.sprite = icon;
    }
}

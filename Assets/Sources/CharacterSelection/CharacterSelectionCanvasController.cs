using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionCanvasController : MonoBehaviour
{
    private const string LOCKED_TEXT = "Locked";
    private const string UNLOCKED_TEXT = "Unlocked";

    [Header("References")]
    public Image characterIcon = null;
    public TextMeshProUGUI characterName = null;
    public TextMeshProUGUI characterStatus = null;
    public Image characterStatusBackground = null;

    [Header("Properties")]
    public Color colorLocked = Color.white;
    public Color colorUnlocked = Color.white;

    public void UpdateContent(string name, Sprite icon, bool isUnlock)
    {
        characterName.text = name;
        characterIcon.sprite = icon;

        characterStatus.text = isUnlock == true ? UNLOCKED_TEXT : LOCKED_TEXT;
        characterStatusBackground.color = isUnlock == true ? colorUnlocked : colorLocked;
    }
}
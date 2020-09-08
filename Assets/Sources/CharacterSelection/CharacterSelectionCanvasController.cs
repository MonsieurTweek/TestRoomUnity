using TMPro;
using UnityEngine;

public class CharacterSelectionCanvasController : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI characterName = null;
    public TextMeshProUGUI characterStatus = null;
    public GameObject confirmButton = null;

    public void UpdateContent(string name, Sprite icon, bool isUnlock)
    {
        characterName.text = name;

        characterStatus.transform.parent.gameObject.SetActive(!isUnlock);
        confirmButton.SetActive(isUnlock);
    }
}
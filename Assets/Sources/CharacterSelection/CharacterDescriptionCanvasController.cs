using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDescriptionCanvasController : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI title = null;
    public TextMeshProUGUI description = null;
    public TextMeshProUGUI lightAbility = null;
    public TextMeshProUGUI heavyAbility = null;
    public TextMeshProUGUI passiveAbility = null;

    public Image icon = null;

    private RectTransform _rectTransform = null;
    private CanvasGroup _canvasGroup = null;
    private CharacterSelectedController _currentCharacter = null;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();

        _canvasGroup.alpha = 0f;
    }

    public void OnCharacterSelected(CharacterSelectedController character)
    {
        _currentCharacter = character;

        Hide();
    }

    private void Hide()
    {
        LeanTween.alphaCanvas(_canvasGroup, 0f, 0.25f).setOnComplete(OnHiddenComplete);
    }

    private void OnHiddenComplete()
    {
        UpdateContent();
        UpdatePosition();

        LeanTween.alphaCanvas(_canvasGroup, 1f, 0.25f);
    }

    private void UpdateContent()
    {
        title.text = _currentCharacter.title;
        lightAbility.text = _currentCharacter.lightAbilityDesc;
        heavyAbility.text = _currentCharacter.heavyAbilityDesc;
        passiveAbility.text = _currentCharacter.passiveAbilityDesc;

        icon.sprite = _currentCharacter.icon;
    }

    private void UpdatePosition()
    {
        _rectTransform.position = _currentCharacter.canvasAnchor.position;
        _rectTransform.rotation = _currentCharacter.canvasAnchor.rotation;
    }
}

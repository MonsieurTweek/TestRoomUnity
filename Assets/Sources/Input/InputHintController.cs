using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputHintController : MonoBehaviour
{
    [Header("References")]
    public Image image = null;
    public TextMeshProUGUI text = null;

    [Header("Keyboard")]
    [Header("Properties")]
    public Sprite keyboardIcon = null;
    public string keyboardText = string.Empty;

    [Header("Gamepad")]
    public Sprite gamepadIcon = null;
    public string gamepadText = string.Empty;

    private void Awake()
    {
        if (text != null)
        {
            text.gameObject.SetActive(false);
        }

        if (image != null)
        {
            image.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        GameEvent.instance.onControlSwitched += OnControlSwitched;

        OnControlSwitched(InputManager.instance.isKeyboard);
    }

    private void OnControlSwitched(bool isKeyboard)
    {
        if (text != null)
        {
            text.text = isKeyboard == true ? keyboardText : gamepadText;

            text.gameObject.SetActive(true);
        }

        if (image != null)
        {
            image.sprite = isKeyboard == true ? keyboardIcon : gamepadIcon;

            image.gameObject.SetActive(image.sprite != null);
        }
    }

    private void OnDestroy()
    {
        if (GameEvent.instance != null)
        {
            GameEvent.instance.onControlSwitched -= OnControlSwitched;
        }
    }
}
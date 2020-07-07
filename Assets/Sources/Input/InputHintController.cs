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
        }

        if (image != null)
        {
            image.sprite = isKeyboard == true ? keyboardIcon: gamepadIcon;
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
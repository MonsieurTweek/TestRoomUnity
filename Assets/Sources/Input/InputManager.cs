using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance { private set; get; }

    public PlayerControls controls { private set; get; }
    public bool isKeyboard { private set; get; }

    public PlayerControls.GameplayActions gameplay { get { return controls.Gameplay; } }
    public PlayerControls.MenuActions menu { get { return controls.Menu; } }
    public PlayerControls.CheatActions cheat { get { return controls.Cheat; } }

    private void Awake()
    {
        // First destroy any existing instance of it
        if (instance != null)
        {
            Destroy(instance);
        }

        // Then reassign a proper one
        instance = this;

        if( controls == null)
        {
            InitializeControls();
        }

        isKeyboard = Gamepad.current == null;
    }

    private void InitializeControls()
    {
        // Assign player controls
        controls = new PlayerControls();

        menu.Ping_Keyboard.performed += OnPingKeyboardPerformed;
        menu.Ping_Gamepad.performed += OnPingGamepadPerformed;
    }

    private void OnPingKeyboardPerformed(InputAction.CallbackContext context)
    {
        isKeyboard = true;

        GameEvent.instance.ControlSwitched(isKeyboard);
    }

    private void OnPingGamepadPerformed(InputAction.CallbackContext context)
    {
        isKeyboard = false;

        GameEvent.instance.ControlSwitched(isKeyboard);
    }

    public void SetVibration(float lowFrequency, float highFrequency)
    {
        if (isKeyboard == false && Gamepad.current != null)
        {
            Gamepad.current.SetMotorSpeeds(lowFrequency, highFrequency);
        }
    }

    private void OnEnable()
    {
        if (controls == null)
        {
            InitializeControls();
        }

        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();

        // Ensure we don't end up with vibration on when stop playing
        SetVibration(0f, 0f);
    }
}
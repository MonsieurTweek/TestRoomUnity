using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CustomizationController : MonoBehaviour
{
    public static CustomizationController instance { private set; get; }

    [SerializeField]
    private PlayerCustomizationController _playerCustomizationController = null;
    public PlayerCustomizationController playerCustomizationController { get { return _playerCustomizationController; } }

    [SerializeField]
    private CustomizationPartOnGUI _firstPart = null;

    [SerializeField]
    protected Toggle _toggleButton = null;

    [SerializeField]
    protected Button _saveButton = null;
    [SerializeField]
    protected Button _loadButton = null;

    public void Start()
    {
        if (CustomizationController.instance == null)
        {
            CustomizationController.instance = this;

            // Force initialization on Male
            CustomizationController.instance._toggleButton.onValueChanged.AddListener(ToggleMale);
            CustomizationController.instance._saveButton.onClick.AddListener(SaveCustomization);
            CustomizationController.instance._loadButton.onClick.AddListener(LoadCustomization);

            StartCoroutine("LateInitialization");
            
        }
        else
        {
            throw new System.Exception("One instance of CustomizationController already exists on game object " + this.gameObject);
        }
    }

    public IEnumerator LateInitialization()
    {
        yield return new WaitForEndOfFrame();

        bool currentToggle = _toggleButton.isOn;

        if (_playerCustomizationController.customization != null)
        {
            _toggleButton.isOn = _playerCustomizationController.customization.isMale;
        }
        else
        {
            _toggleButton.isOn = true;
        }

        // Force toggle in case it has not been changed
        if (currentToggle == _toggleButton.isOn)
        {
            ToggleMale(currentToggle);
        }

        // Force initialization on a specific part
        if (_firstPart != null)
        {
            CustomizationPartOnGUI.currentPart = _firstPart;
            CustomizationPartOnGUI.currentPart.RefreshGrid();
        }
    }

    public void ToggleMale(bool isMale)
    {
        _playerCustomizationController.isMale = isMale;

        if (CustomizationPartOnGUI.currentPart != null)
        {
            CustomizationPartOnGUI.currentPart.RefreshGrid();
        }

        // Show corresponding layer
        Camera.main.cullingMask |= isMale == true ? 1 << PlayerCustomizationController.CUSTOMIZATION_MALE_LAYER : 1 << PlayerCustomizationController.CUSTOMIZATION_FEMALE_LAYER;
        // Hide opposite layer
        Camera.main.cullingMask &= isMale == false ? ~(1 << PlayerCustomizationController.CUSTOMIZATION_MALE_LAYER) : ~(1 << PlayerCustomizationController.CUSTOMIZATION_FEMALE_LAYER);
    }

    public void SaveCustomization()
    {
        _playerCustomizationController.SaveCustomization();
    }

    public void LoadCustomization()
    {
        _playerCustomizationController.LoadCustomizationFromPrefs();
    }
}
using System;
using UnityEngine;
using UnityEngine.UI;

public class CustomizationController : MonoBehaviour
{
    private const int CUSTOMIZATION_MALE_LAYER = 10;
    private const int CUSTOMIZATION_FEMALE_LAYER = 11;

    private static CustomizationController _instance = null;
    public static CustomizationController instance { get { return _instance; } set { _instance = value; } }

    public bool isMale = true;

    [SerializeField]
    private CustomizationPartController _firstPart = null;

    [SerializeField]
    protected Toggle _toggleButton = null;

    public void Start()
    {
        if (CustomizationController.instance == null)
        {
            CustomizationController.instance = this;

            // Force initialization on Male
            CustomizationController.instance._toggleButton.onValueChanged.AddListener(ToggleMale);
            ToggleMale(true);

            // Force initialization on a specific part
            if (_firstPart != null)
            {
                CustomizationPartController.currentPart = _firstPart;
                CustomizationPartController.currentPart.RefreshGrid();
            }
        }
        else
        {
            throw new System.Exception("One instance of CustomizationController already exists on game object " + this.gameObject);
        }
    }

    public void ToggleMale(bool toggle)
    {
        isMale = toggle;

        _toggleButton.isOn = isMale == true;

        if (CustomizationPartController.currentPart != null)
        {
            CustomizationPartController.currentPart.RefreshGrid();
        }

        // Show corresponding layer
        Camera.main.cullingMask |= isMale == true ? 1 << CUSTOMIZATION_MALE_LAYER : 1 << CUSTOMIZATION_FEMALE_LAYER;
        // Hide opposite layer
        Camera.main.cullingMask &= isMale == false ? ~(1 << CUSTOMIZATION_MALE_LAYER) : ~(1 << CUSTOMIZATION_FEMALE_LAYER);
    }
}
﻿using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage customization on a character (apply skin and gender)
/// </summary>
public class PlayerCustomizationController : MonoBehaviour
{
    // Layers to enable parts based on gender
    public static readonly int CUSTOMIZATION_MALE_LAYER = 10;
    public static readonly int CUSTOMIZATION_FEMALE_LAYER = 11;

    private bool _isMale = true;
    /// <summary>
    /// Manage character gender. Setting it set parts as dirty.
    /// </summary>
    public bool isMale
    {
        get
        {
            return _isMale;
        }
        set
        {
            foreach(CustomizationPartOnPlayer part in customizableParts)
            {
                part.genderHasChanged = true;
            }

            _isMale = value;
        }
    }

    private int _currentCustomizationIndex = 0;
    /// <summary>
    /// Index to the customization selected. Setting it can't exceed customizations length
    /// </summary>
    public int currentCustomizationIndex
    {
        get
        {
            return _currentCustomizationIndex;
        }
        set
        {
            _currentCustomizationIndex = value % customizations.Length;
        }
    }

    public bool loadFromPrefs = false;

    /// <summary>
    /// Array of scriptable object containing the skin list and gender
    /// </summary>
    public Customization[] customizations = null;
    public Customization customization { get { return _currentCustomizationIndex < customizations.Length ? customizations[_currentCustomizationIndex] : null; } }

    [HideInInspector]
    public List<CustomizationPartOnPlayer> customizableParts = new List<CustomizationPartOnPlayer>();

    private void Start()
    {
        if (loadFromPrefs == true)
        {
            LoadCustomizationFromPrefs();
        }
        else if (customization != null)
        {
            LoadCustomizationFromPreset(customization);
        }
    }

    /// <summary>
    /// Get key to the customization saved in player prefs depending on gender
    /// </summary>
    /// <returns></returns>
    public string GetCustomizationPref()
    {
        if (isMale == true)
        {
            return "PLAYER_CUSTOMIZATION_" + CUSTOMIZATION_MALE_LAYER;
        }
        else
        {
            return "PLAYER_CUSTOMIZATION_" + CUSTOMIZATION_FEMALE_LAYER;
        }
    }

    public string GetSkinKeyToPref()
    {
        return GetCustomizationPref() + "_SKINS";
    }

    public string GetGenderKeyToPref()
    {
        return "PLAYER_CUSTOMIZATION_GENDER";
    }

    /// <summary>
    /// Load customization settings from a scriptable object preset
    /// </summary>
    public void LoadCustomizationFromPreset(Customization preset)
    {
        isMale = preset.isMale;

        Material materialToApply = null;

        foreach (CustomizationPartOnPlayer part in customizableParts)
        {
            part.ResetPart();

            foreach (Skin skin in preset.skins.list)
            {
                if (skin.name == part.uniqueName)
                {
                    materialToApply = skin.material != null ? skin.material : preset.material;

                    part.SetPart(skin.index);

                    if (materialToApply != null)
                    {
                        part.SetMaterial(materialToApply, false);
                    }

                    break;
                }
            }
        }
    }

    public void LoadCustomizationFromPrefs()
    {
        isMale = PlayerPrefs.GetInt(GetGenderKeyToPref(), 1) == 1;

        string skinListSerialized = PlayerPrefs.GetString(GetSkinKeyToPref());

        Customization preset = ScriptableObject.CreateInstance<Customization>();

        preset.isMale = isMale;
        preset.skins = JsonUtility.FromJson<SkinList>(skinListSerialized);

        LoadCustomizationFromPreset(preset);
    }

    public void SaveCustomization()
    {
        Customization customization = ScriptableObject.CreateInstance<Customization>();

        customization.isMale = isMale;
        customization.Populate(customizableParts);

        PlayerPrefs.SetInt(GetGenderKeyToPref(), Convert.ToInt32(isMale));
        PlayerPrefs.SetString(GetSkinKeyToPref(), JsonUtility.ToJson(customization.skins));
    }
}
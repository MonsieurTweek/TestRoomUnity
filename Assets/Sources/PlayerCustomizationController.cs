using System.Collections.Generic;
using UnityEngine;

public class PlayerCustomizationController : MonoBehaviour
{
    public static readonly int CUSTOMIZATION_MALE_LAYER = 10;
    public static readonly int CUSTOMIZATION_FEMALE_LAYER = 11;

    private bool _isMale = true;
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

    public Customization customization = null;

    [HideInInspector]
    public List<CustomizationPartOnPlayer> customizableParts = new List<CustomizationPartOnPlayer>();

    private void Start()
    {
        if (customization != null)
        {
            isMale = customization.isMale;
            LoadCustomizationFromPreset();
        }
    }

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

    public void LoadCustomizationFromPreset()
    {
        Material materialToApply = null;

        foreach (CustomizationPartOnPlayer part in customizableParts)
        {
            part.ResetPart();

            foreach (Skin skin in customization.skins.list)
            {
                if (skin.name == part.uniqueName)
                {
                    materialToApply = skin.material != null ? skin.material : customization.material;

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

    public void LoadCustomizationFromSave()
    {
        string skinListSerialized = PlayerPrefs.GetString(GetCustomizationPref());

        SkinList skinList = JsonUtility.FromJson<SkinList>(skinListSerialized);

        foreach (CustomizationPartOnPlayer part in customizableParts)
        {
            part.ResetPart();

            foreach (Skin skin in skinList.list)
            {
                if (skin.name == part.uniqueName)
                {
                    part.SetPart(skin.index);
                    //part.SetMaterial(skin.material, false);
                    break;
                }
            }
        }
    }
}

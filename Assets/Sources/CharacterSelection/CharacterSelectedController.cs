using Cinemachine;
using UnityEngine;

/// <summary>
/// Manage the individual character selection
/// </summary>
public class CharacterSelectedController : MonoBehaviour
{
    public Archetype archetype = null;
    public PlayerCustomizationController character = null;
    public CinemachineVirtualCamera virtualCamera = null;
    public RectTransform canvasAnchor = null;

    public string title { get { return archetype.title; } }
    public string description { get { return archetype.description; } }
    public string lightAbilityDesc { get { return archetype.lightAbilityDescription; } }
    public string heavyAbilityDesc { get { return archetype.heavyAbilityDescription; } }
    public string passiveAbilityDesc { get { return archetype.passiveAbilityDescription; } }
    public Sprite icon { get { return archetype.icon; } }
    public bool isPlayable { get { return archetype.isPlayable; } }
    public bool isUnlock { get { return (SaveData.current.playerProfile.characters & (uint)archetype.character) != 0; } }

    public void Deselect()
    {
        virtualCamera.Priority = 10;
    }

    public void Select()
    {
        virtualCamera.Priority = 100;
    }

    public bool Validate()
    {
        if (isUnlock == true)
        {
            character.SaveCustomization();
            archetype.Save(Archetype.SAVE_PATH);

            return true;
        }

        return false;
    }

    public void ChangeCustomization()
    {
        character.currentCustomizationIndex++;
        character.LoadCustomizationFromPreset(character.customization);
    }
}

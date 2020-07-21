using Cinemachine;
using UnityEngine;

/// <summary>
/// Manage the individual character selection
/// </summary>
public class CharacterSelectedController : MonoBehaviour
{
    public string title { get { return _archetype.title; } }
    public Sprite icon { get { return _archetype.icon; } }
    public bool isUnlock { get { return (SaveData.current.playerProfile.characters & (uint)_archetype.character) != 0; } }

    [SerializeField]
    private Archetype _archetype = null;

    [SerializeField]
    private PlayerCustomizationController _character = null;

    [SerializeField]
    private CinemachineVirtualCamera _camera = null;

    public void Deselect()
    {
        _camera.Priority = 10;
    }

    public void Select()
    {
        _camera.Priority = 100;
    }

    public bool Validate()
    {
        if (isUnlock == true)
        {
            _character.SaveCustomization();
            _archetype.Save(Archetype.SAVE_PATH);

            return true;
        }

        return false;
    }

    public void ChangeCustomization()
    {
        _character.currentCustomizationIndex++;
        _character.LoadCustomizationFromPreset(_character.customization);
    }
}

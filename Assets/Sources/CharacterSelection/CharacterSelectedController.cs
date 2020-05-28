using Cinemachine;
using UnityEngine;

/// <summary>
/// Manage the individual character selection
/// </summary>
public class CharacterSelectedController : MonoBehaviour
{
    public string title { get { return _archetype.title; } }
    public Sprite icon { get { return _archetype.icon; } }

    [SerializeField]
    private Archetype _archetype = null;

    [SerializeField]
    private PlayerCustomizationController _character = null;

    [SerializeField]
    private Light _light = null;

    [SerializeField]
    private CinemachineVirtualCamera _camera = null;

    private void Awake()
    {
        Deselect();
    }

    public void Deselect()
    {
        _camera.Priority = 10;
        _light.intensity = 0;
    }

    public void Select()
    {
        _camera.Priority = 100;
        _light.intensity = 1;
    }

    public void Validate()
    {
        _character.SaveCustomization();
    }

    public void ChangeCustomization()
    {
        _character.currentCustomizationIndex++;
        _character.LoadCustomizationFromPreset(_character.customization);
    }
}

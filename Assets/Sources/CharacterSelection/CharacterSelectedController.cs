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
    private float _lightIntensity = 0f;
    private float _lightIntensityAnimation = 0f;

    [SerializeField]
    private CinemachineVirtualCamera _camera = null;

    private void Awake()
    {
        _lightIntensity = _light.intensity;
    }

    public void Deselect()
    {
        _camera.Priority = 10;
        _lightIntensityAnimation = 0f;
    }

    public void Select()
    {
        _camera.Priority = 100;
        _light.enabled = true;
        _lightIntensityAnimation = _lightIntensity;
    }

    public void Validate()
    {
        _character.SaveCustomization();
        _archetype.Save(Archetype.SAVE_PATH);
    }

    public void ChangeCustomization()
    {
        _character.currentCustomizationIndex++;
        _character.LoadCustomizationFromPreset(_character.customization);
    }

    private void Update()
    {
        _light.intensity = Mathf.Lerp(_light.intensity, _lightIntensityAnimation, 2f * Time.deltaTime);
    }
}

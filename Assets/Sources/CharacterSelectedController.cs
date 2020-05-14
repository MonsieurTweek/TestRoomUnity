using Cinemachine;
using UnityEngine;

public class CharacterSelectedController : MonoBehaviour
{
    //TODO: Create a struct instead of fields for name and icon
    public string name = null;
    public Sprite icon = null;


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

    }
}

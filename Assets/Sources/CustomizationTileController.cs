using System;
using UnityEngine;
using UnityEngine.UI;

public class CustomizationTileController : MonoBehaviour
{
    private static uint ID_PROVIDER = 0;

    [SerializeField] private RawImage _icon = null;
    [SerializeField] private Image _background = null;
    [SerializeField] private Button _button = null;
    [SerializeField] private uint _index = 0;

    public RawImage icon {  get { return _icon; } }
    public Image background { get { return _background; } }
    public Button button { get { return _button; } }
    public uint index{ get { return _index; } }

    private void Awake()
    {
        _index = CustomizationTileController.ID_PROVIDER++;

        CustomizationGridController.instance.onTileSelected += OnTileSelected;
    }

    private void OnTileSelected(uint index)
    {
        _background.color = index == _index ? Color.gray : Color.white;
    }
}
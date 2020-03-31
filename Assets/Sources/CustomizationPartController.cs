using UnityEngine;

public class CustomizationPartController : MonoBehaviour
{
    public static CustomizationPartController currentPart = null;

    [SerializeField]
    private GameObject _bone = null;
    public GameObject bone { get { return _bone; } }

    [SerializeField]
    private Vector3 _cameraOffset = Vector3.zero;

    [SerializeField]
    protected GameObject[] _maleSkins = null;

    [SerializeField]
    protected GameObject[] _femaleSkins = null;

    private GameObject[] _skins = null;

    private uint _currentPartIndex = 0;

    public void RefreshGrid()
    {
        GetSkinsByGender();

        CustomizationPartController.currentPart = this;

        CustomizationGridController.RefreshGrid(_skins, _cameraOffset);
    }

    public void SetPart(uint index)
    {
        _skins[_currentPartIndex].SetActive(false);

        _currentPartIndex = index;

        _skins[_currentPartIndex].SetActive(true);
    }

    public void GetSkinsByGender()
    {
        GameObject[] skins = CustomizationController.instance.isMale == true ? _maleSkins : _femaleSkins;

        if (_skins != null)
        {
            int offset = skins.Length - _skins.Length;

            _currentPartIndex = (uint) Mathf.Max(0, _currentPartIndex + offset);
        }

        _skins = skins;
    }
}
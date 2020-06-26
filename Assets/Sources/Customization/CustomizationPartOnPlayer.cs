using UnityEngine;

public class CustomizationPartOnPlayer : MonoBehaviour
{
    [SerializeField]
    private PlayerCustomizationController _controller = null;

    [SerializeField]
    private bool _firstSkinIsDefault = true;

    [SerializeField]
    private GameObject[] _maleSkins = null;

    [SerializeField]
    private GameObject[] _femaleSkins = null;

    private GameObject[] _elements = null;
    public GameObject[] elements
    {
        get
        {
            if (genderHasChanged == true)
            {
                GetSkinsByGender();

                genderHasChanged = false;
            }

            return _elements;
        }
    }

    private Skin _skin = new Skin();
    public Skin skin { get { return _skin; } }

    private uint _currentPartIndex = 0;
    public uint currentPartIndex { get { return _currentPartIndex; } }

    private string _uniqueName = string.Empty;
    public string uniqueName { get { return _uniqueName; } }

    [HideInInspector]
    public bool genderHasChanged = true;

    private void Awake()
    {
        // Register this part as one of the parts we can customize
        _controller.customizableParts.Add(this);

        _uniqueName = transform.parent.name + "_" + gameObject.name;
    }

    public void ResetPart()
    {
        // For each skin
        for (int i = 0; i < _maleSkins.Length; i++)
        {
            // disable it
            _maleSkins[i].SetActive(false);
        }

        // if we have a default as first skin
        if (_firstSkinIsDefault == true && _maleSkins.Length > 0 && _controller.isMale == true)
        {
            // enable this one
            _maleSkins[0].SetActive(true);

            _currentPartIndex = 0;
        }

        // For each skin
        for (int i = 0; i < _femaleSkins.Length; i++)
        {
            // disable it
            _femaleSkins[i].SetActive(false);
        }

        // if we have a default as first skin
        if (_firstSkinIsDefault == true && _femaleSkins.Length > 0 && _controller.isMale == false)
        {
            // enable this one
            _femaleSkins[0].SetActive(true);

            _currentPartIndex = 0;
        }
    }

    public void SetPart(uint index)
    {
        if (index < elements.Length)
        {
            // Disable existing only if it was used
            if (_currentPartIndex < elements.Length)
            {
                elements[_currentPartIndex].SetActive(false);
            }

            _currentPartIndex = index;

            // Save index for preset export
            _skin.name = uniqueName;
            _skin.index = index;

            elements[_currentPartIndex].SetActive(true);
        }
        else
        {
            UnityEngine.Debug.LogWarning("Can't find index " + index + " for part " + gameObject.name);
        }
    }

    public void SetMaterial(Material material, bool refreshGrid = true)
    {
        Material mat = new Material(material);

        foreach (GameObject skin in elements)
        {
            skin.GetComponent<SkinnedMeshRenderer>().material = mat;
        }

        // Save material for preset export
        _skin.material = material;
    }

    public void SetAdditionalMaterial(Material material)
    {
        SkinnedMeshRenderer mesh = elements[_currentPartIndex].GetComponent<SkinnedMeshRenderer>();

        Material[] matArray = new Material[mesh.materials.Length + 1];

        for (int i = 0; i < mesh.materials.Length; i++)
        {
            matArray[i] = mesh.materials[i];
        }

        matArray[mesh.materials.Length] = material;
        mesh.materials = matArray;
    }

    public void GetSkinsByGender()
    {
        GameObject[] skins = _controller.isMale == true ? _maleSkins : _femaleSkins;

        if (_elements != null)
        {
            int offset = skins.Length - _elements.Length;

            _currentPartIndex = (uint) Mathf.Max(0, _currentPartIndex + offset);
        }

        _elements = skins;
    }
}
using UnityEngine;

public class CustomizationPartOnGUI : MonoBehaviour
{
    // GUI
    public static CustomizationPartOnGUI currentPart = null;
    public static Material currentMaterial = null;

    [SerializeField]
    private CustomizationPartOnPlayer _partOnPlayer = null;
    public CustomizationPartOnPlayer partOnPlayer { get { return _partOnPlayer; } }

    public GameObject bone { get { return _partOnPlayer.gameObject; } }

    [SerializeField]
    private Vector3 _cameraOffset = Vector3.zero;

    private Skin _skin = new Skin();

    public void RefreshGrid()
    {
        CustomizationPartOnGUI.currentPart = this;

        if (CustomizationPartOnGUI.currentMaterial != null)
        {
            SetMaterial(CustomizationPartOnGUI.currentMaterial, false);
        }

        CustomizationGridController.RefreshGrid(_partOnPlayer.elements, _cameraOffset);
    }

    public void SetPart(uint index)
    {
        _partOnPlayer.SetPart(index);
    }

    public void SetMaterial(Material material, bool refreshGrid = true)
    {
        _partOnPlayer.SetMaterial(material);

        CustomizationPartOnGUI.currentMaterial = material;

        // Refresh grid only if necessary (as loading may have not all information to refresh grids)
        if (refreshGrid == true)
        {
            CustomizationGridController.RefreshGrid(_partOnPlayer.elements, _cameraOffset);
        }
    }
}
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class CustomizationPartController : MonoBehaviour
{
    public static CustomizationPartController currentBone = null;

    [SerializeField]
    private Vector3 _cameraOffset = Vector3.zero;

    [SerializeField]
    protected GameObject[] _parts = null;

    private uint _currentPartIndex = 0;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) == true && GetClick() == true)
        {
            if (CustomizationPartController.currentBone != this)
            {
                CustomizationPartController.currentBone = this;

                CustomizationGridController.RefreshGrid(_parts, _cameraOffset);
            }
        }
    }

    private bool GetClick()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            return gameObject == hit.collider.gameObject;
        }

        return false;
    }

    public void SetPart(uint index)
    {
        _parts[_currentPartIndex].SetActive(false);

        _currentPartIndex = index;

        _parts[_currentPartIndex].SetActive(true);
    }
}
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class CustomizationPartController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _parts = null;

    private uint _currentPartIndex = 0;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) == true && GetClick() == true)
        {
            _parts[_currentPartIndex].SetActive(false);

            if (++ _currentPartIndex >= _parts.Length)
            {
                _currentPartIndex = 0;
            }

            _parts[_currentPartIndex].SetActive(true);
        }
    }

    private bool GetClick()
    {
        // Did we hit the surface?
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

        if (Physics.Raycast(ray, out hit))
        {
            //  Do whatever you want to detect what's been hit from the data stored in the "hit" variable - this should rotate it...

            return gameObject == hit.collider.gameObject;

        }

        return false;
    }
}

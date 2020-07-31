using System.Collections.Generic;
using UnityEngine;

public class CameraHideController : MonoBehaviour
{
    private const int HIDE_LAYER = 15;

    public Transform target = null;
    public LayerMask layerMask = new LayerMask();

    private Dictionary<GameObject, int> _objectsHiddenToLayer = new Dictionary<GameObject, int>();

    private void FixedUpdate()
    {
        Vector3 direction = target.position + Vector3.up - transform.position;
        List<GameObject> objectsToHide = new List<GameObject>();
        List<GameObject> objectsToShow = new List<GameObject>();

#if UNITY_EDITOR
        Debug.DrawRay(transform.position, direction, Color.blue);
#endif

        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, direction.magnitude, layerMask);

        // Find new objects to hide
        foreach (RaycastHit hit in hits)
        {
            objectsToHide.Add(hit.collider.gameObject);
        }

        // Find hidden objects to show
        foreach (GameObject gameObject in _objectsHiddenToLayer.Keys)
        {
            if (objectsToHide.Contains(gameObject) == false)
            {
                objectsToShow.Add(gameObject);
            }
            else
            {
                objectsToHide.Remove(gameObject);
            }
        }

        // Show objects
        foreach (GameObject gameObjectToShow in objectsToShow)
        {
            // Reassign original layer 
            gameObjectToShow.layer = _objectsHiddenToLayer[gameObjectToShow];

            _objectsHiddenToLayer.Remove(gameObjectToShow);
        }

        // Hide objects
        foreach (GameObject gameObjectToHide in objectsToHide)
        {
            // TODO : Check why we are adding same object several times
            _objectsHiddenToLayer[gameObjectToHide] = gameObjectToHide.layer;

            // Reassign Hidden layer 
            gameObjectToHide.layer = HIDE_LAYER;
        }
    }
}
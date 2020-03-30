using UnityEngine;

public class CustomizationRootController : MonoBehaviour
{
    public float rotationSpeed = 1f;  //This will determine max rotation speed, you can adjust in the inspector
    public float sensibility = 1f;
    public float treshold = 0.1f;

    [SerializeField]
    private GameObject _root;  // Drag object to rotate here

    private float _origin = 0f;
    private bool _isMovementStarted = false;

    private void Update()
    {
        // Rotate on mouse down
        if (Input.GetMouseButton(0) == true)
        {
            if (_isMovementStarted == false)
            {
                _isMovementStarted = true;
                _origin = Input.mousePosition.x;
            }

            RotateObject();
        }
        else if (Input.GetMouseButtonUp(0) == true)
        {
            _isMovementStarted = false;
        }
    }

    private void RotateObject()
    {
        //Get mouse position
        Vector3 mousePos = Input.mousePosition;

        float delta = _origin - mousePos.x;

        if (Mathf.Abs(delta) > treshold)
        {
            //Get the angle to rotate and rotate
            float angle = _root.transform.localRotation.y + (_origin - mousePos.x) / sensibility;

            _root.transform.localRotation = Quaternion.Slerp(_root.transform.localRotation, Quaternion.Euler(0, angle, 0), rotationSpeed * Time.deltaTime);
        }
    }
}
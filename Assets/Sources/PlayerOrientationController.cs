using UnityEngine;

public class PlayerOrientationController : MonoBehaviour
{
    private Vector2 _playerRotation = Vector2.zero;
    public float speed = 3f;

    private void Awake()
    {
        // Save current player rotation to start with
        _playerRotation = transform.eulerAngles;
    }

    private void Update()
    {
        // On unlock camera
        if (Input.GetMouseButton(1) == true)
        {
            // Compute rotation based on mouse horizontal position
            _playerRotation.y += Input.GetAxis("Mouse X");

            // Apply it to player rotation
            transform.eulerAngles = _playerRotation * speed;
        }
    }
}
using UnityEngine;

public class DummyRotationController : MonoBehaviour
{
    public float speed = 1f;
    public Vector3 direction = Vector3.forward;

    private void Update()
    {
        transform.Rotate(direction * speed * Time.deltaTime);
    }
}
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerLocomotionController : MonoBehaviour
{
    private Animator _animator;
    private CharacterController _characterController;

    private Vector3 _velocity = Vector3.zero;
    private Vector3 _direction = Vector3.zero;
    private bool _isJumping = false;

    public float movementSpeed = 3f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        ComputeDirection();

        Animate();
    }

    private void ComputeDirection()
    {
        // Switch on/off walk modifier
        float modifier = Input.GetKey(KeyCode.LeftShift) == true ? 0.5f : 1f;

        // Get velocity based on input
        _velocity.z = Input.GetAxis("Vertical") * modifier;
        _velocity.x = Input.GetAxis("Horizontal") * modifier;

        // Apply velocity to forward direction
        _direction = transform.forward * _velocity.z * movementSpeed;

        // Apply velocity to straf direction
        _direction += transform.right * _velocity.x * movementSpeed;
    }

    private void Animate()
    {
        _animator.SetFloat("VelocityX", _velocity.x);
        _animator.SetFloat("VelocityY", _velocity.z);
    }

    private void FixedUpdate()
    {
        if (_characterController.isGrounded == true)
        {
            if (_isJumping == true)
            {
                _isJumping = false;
            }

            if (Input.GetButton("Jump") == true)
            {
                _direction.y = jumpSpeed;
                _isJumping = true;
            }
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        _direction.y -= gravity * Time.deltaTime;

        // Move the controller
        _characterController.Move(_direction * Time.deltaTime);
    }
}

using UnityEngine;

public class DummyJumpController : MonoBehaviour
{
    private bool _isGrounded = false;
    private bool _isJumping = false;
    private bool _canJump = true;

    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 _input = Vector3.zero;
    private Vector3 _velocity = Vector3.zero;

    private void Update()
    {
        
    }
    private void FixedUpdate()
    {

        /*if (_isGrounded == true)
        {
            // Reset jump properties when grounded
            _velocity.y = 0f;
            _isJumping = false;

            // Jump
            if (Input.GetKeyUp(KeyCode.Space) == true)
            {
                _isJumping = true;

                // Impulse jump velocity
                _velocity += Vector3.up * jumpSpeed;
            }
        }*/

        CheckGrounded();

        MoveAlt();


    }

    private void Move()
    {
        Vector3 targetPosition = transform.position + _velocity;

        targetPosition.y = 0f;

        // Move to the new position
        transform.Translate((targetPosition - transform.position) * Time.deltaTime);

        transform.localPosition += transform.up * _velocity.y * Time.deltaTime;

        // Apply gravity after applying movement once so first jump impulsion take player off the ground
        if (_isGrounded == false)
        {
            _velocity -= Vector3.up * gravity * Time.deltaTime;
        }
    }

    private void MoveAlt()
    {
        if (_isGrounded == true)
        {
            _velocity.y = 0f;

            if (_isJumping == true)
            {
                _isJumping = false;
                _canJump = true;
            }

            // We are grounded, so recalculate
            // move direction directly from axes

            if (Input.GetButton("Jump") == true && _canJump == true)
            {
                _velocity.y = jumpSpeed;
                _isJumping = true;
                _canJump = false;
            }
        }
        
        if (_isGrounded == false || _isJumping == true)
        {
            // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
            // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
            // as an acceleration (ms^-2)
            _velocity.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        transform.localPosition += transform.up * _velocity.y * Time.deltaTime;
    }

    private void CheckGrounded()
    {
        RaycastHit hit;
        float rayLength = 10.0f; //use any value.
                                 //shoot a debug ray down to show you the ray it's casting--have to turn gizmos on in gameView window

        Debug.DrawRay(transform.position, new Vector3(0f, -rayLength, 0f), Color.yellow);

        //cast the actual ray for ground detection
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, rayLength) == true)
        {
            //if the RayCast hit something below the player
            //check the actual distance of the hit object
            _isGrounded = hit.distance <= 0.15f;
        }
        // if nothing found assume player is grounded
        else
        {
            _isGrounded = true;
        }
    }

#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        UnityEditor.Handles.Label(transform.position + Vector3.up * 0.5f, "Is grounded : " + _isGrounded);
        UnityEditor.Handles.Label(transform.position, "Can jump ? " + _canJump + " & is jumping ? " + _isJumping);
    }
#endif
}

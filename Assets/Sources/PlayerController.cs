using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GamepadInput;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    CharacterController characterController;

    public float speed = 6.0f;
    public float rotationSpeed = 100.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float rotation = 0.0f;

    private bool _canJump = true;

    private Vector3 _moveDirection = Vector3.zero;
    private Animator _animator;

    private bool _isJumping = false;

    public bool CanJump { get => _canJump; set => _canJump = value; }

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (characterController.isGrounded == true)
        {

            if(_isJumping == true)
            {
                _isJumping = false;
                _animator.SetBool("Jumping", false);
                _animator.Play("Unarmed-Land");
            }

            // We are grounded, so recalculate
            // move direction directly from axes

            if(Input.GetAxis("Vertical") > 0.0f)
            {

                _moveDirection = Camera.main.transform.forward;
                _moveDirection *= speed;
                _animator.SetBool("Running", true);
            } else
            {
                _moveDirection = new Vector3(0.0f, 0.0f, 0.0f);
                _moveDirection *= speed;
                _animator.SetBool("Running", false);
            }

            if (Input.GetButton("Jump") == true && CanJump == true)
            {
                _moveDirection.y = jumpSpeed;
                _animator.SetBool("Jumping", true);
                _isJumping = true;
                CanJump = false;
            }
        }

        rotation += Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rotation, 0);

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        _moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(_moveDirection * Time.deltaTime);

    }

}

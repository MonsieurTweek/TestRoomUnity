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

    private Vector3 moveDirection = Vector3.zero;
    private Animator animator;

    private bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (characterController.isGrounded == true)
        {

            if(isJumping == true)
            {
                isJumping = false;
                animator.Play("Unarmed-Idle");
            }


            // We are grounded, so recalculate
            // move direction directly from axes

            if(Input.GetAxis("Vertical") > 0.0f)
            {

                moveDirection = Camera.main.transform.forward;
                moveDirection *= speed;
                animator.SetBool("Running", true);
            } else
            {
                moveDirection = new Vector3(0.0f, 0.0f, 0.0f);
                moveDirection *= speed;
                animator.SetBool("Running", false);
            }

            if (Input.GetButton("Jump") == true)
            {
                moveDirection.y = jumpSpeed;
                animator.Play("Unarmed-Jump");
                isJumping = true;
            }
        }

        rotation += Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rotation, 0);

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

    }
}

using System;
using UnityEngine;

[Serializable]
public class PlayerStateMove : CharacterFSM.CharacterState
{
    private const string ANIMATION_PARAM_VELOCTIY_X = "VelocityX";
    private const string ANIMATION_PARAM_VELOCTIY_Y = "VelocityY";
    private const string ANIMATION_PARAM_JUMP = "Jumping";

    private Vector3 _input = Vector3.zero;
    private Vector3 _velocity = Vector3.zero;
    private bool _isJumping = false;
    private bool _canJump = true;

    public float movementSpeed = 3f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    public override void Update()
    {
        ((PlayerFSM)character).Rotate();

        ComputeDirection();

        Animate();
    }

    private void ComputeDirection()
    {
        // Switch on/off walk modifier
        float modifier = Input.GetKey(KeyCode.LeftShift) == true ? 0.5f : 1f;

        // Get input amplitude
        _input.z = Input.GetAxis("Vertical") * modifier;
        // Apply velocity to straf direction only when targetting
        _input.x = ((PlayerFSM)character).target != null ? Input.GetAxis("Horizontal") * modifier : 0f;

        // Apply input amplitude to forward velocity
        _velocity.z = _input.z * movementSpeed;
        // Apply input amplitude to left velocity
        _velocity.x = _input.x * movementSpeed;
    }

    private void Animate()
    {
        if (((PlayerFSM)character).isGrounded == true)
        {
            character.animator.SetFloat(ANIMATION_PARAM_VELOCTIY_X, _input.x);
            character.animator.SetFloat(ANIMATION_PARAM_VELOCTIY_Y, _input.z);
        }
    }

    public override void FixedUpdate()
    {
        Move();

        Jump();
    }

    private void Move()
    {
        Vector3 targetPosition = character.transform.position + _velocity;

        targetPosition.y = 0f;

        // Move to the new position
        character.transform.Translate((targetPosition - character.transform.position) * Time.deltaTime);
    }

    private void Jump()
    {
        if (((PlayerFSM)character).isGrounded == true)
        {
            _velocity.y = 0f;

            if (_isJumping == true)
            {
                _isJumping = false;
                _canJump = true;
                character.animator.SetBool(ANIMATION_PARAM_JUMP, false);
            }

            // We are grounded, so recalculate
            // move direction directly from axes

            if (Input.GetButton("Jump") == true && _canJump == true)
            {
                _velocity.y = jumpSpeed;
                _isJumping = true;
                _canJump = false;

                character.animator.SetBool(ANIMATION_PARAM_JUMP, true);
            }
        }

        if (((PlayerFSM)character).isGrounded == false || _isJumping == true)
        {
            // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
            // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
            // as an acceleration (ms^-2)
            _velocity.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        ((PlayerFSM)character).model.localPosition += character.transform.up * _velocity.y * Time.deltaTime;
    }

    public override void Exit()
    {
        // Reset _input and _velocity before exiting move state
        _input = Vector3.zero;
        _velocity = Vector3.zero;

        Animate();
    }

#if UNITY_EDITOR
    public override void OnDrawGizmos()
    {
        UnityEditor.Handles.Label(character.transform.position + Vector3.up * 0.5f, "Velocity : " + _velocity);
        UnityEditor.Handles.Label(character.transform.position, "Is grounded : " + ((PlayerFSM)character).isGrounded);
    }
#endif
}
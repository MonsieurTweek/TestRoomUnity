using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class PlayerStateMove : CharacterFSM.CharacterState
{
    private const string ANIMATION_PARAM_VELOCTIY_X = "VelocityX";
    private const string ANIMATION_PARAM_VELOCTIY_Y = "VelocityY";
    private const string ANIMATION_PARAM_JUMP = "Jumping";
    private const string ANIMATION_PARAM_DASH = "Dashing";

    private Vector2 _input = Vector2.zero;
    private Vector3 _velocity = Vector3.zero;

    // Jump properties
    private bool _isJumping = false;
    private bool _isJumpPerformed = false;
    private bool _canJump = true;

    // Dash properties
    private bool _isDashing = false;
    private float _dashTime = float.MaxValue;
    private float _energyForDash = 0f;

    [Header("Speed")]
    public float movementSpeed = 3f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    [Header("Dash")]
    public float dashDistance = 8.0f;
    public float dashDelayIn = 0.25f;
    public float dashDelayOut = 0.25f;
    public ParticleSystem dashFx = null;
    public CinemachineVirtualCamera dashCamera = null;

    private ParticleSystem _currentDashFx = null;

    public override void Enter()
    {
        // Bind controls to actions
        InputManager.instance.gameplay.Jump.performed += Jump;
        InputManager.instance.gameplay.Dash.performed += Dash;

        _energyForDash = ((PlayerData)character.data).energyForDash;
    }

    public override void Update()
    {
        if (_isDashing == false)
        {
            ((PlayerFSM)character).Rotate();

            /*if (Time.time - _dashTime >= _energyForDash)
            {
                _canDash = true;
            }*/
        }

        ComputeDirection();

        Animate();
    }

    private void ComputeDirection()
    {
        // Get input amplitude
        _input.y = InputManager.instance.gameplay.Move.ReadValue<float>();
        
        // Apply velocity to straf direction only when targetting
        _input.x = ((PlayerFSM)character).target != null ? InputManager.instance.gameplay.Strafe.ReadValue<float>() : 0f;

        // Apply input amplitude to forward velocity
        _velocity.z = _input.y * movementSpeed;
        // Apply input amplitude to left velocity
        _velocity.x = _input.x * movementSpeed;
    }

    private void Animate()
    {
        if (((PlayerFSM)character).isGrounded == true)
        {
            character.animator.SetFloat(ANIMATION_PARAM_VELOCTIY_X, _input.x);
            character.animator.SetFloat(ANIMATION_PARAM_VELOCTIY_Y, _input.y);
        }
    }

    public override void FixedUpdate()
    {
        if (_isDashing == false)
        {
            EvaluateMove();

            EvaluateJump();
        }
    }

    private void EvaluateMove()
    {
        Vector3 targetPosition = character.transform.position + _velocity;

        targetPosition.y = 0f;

        // Move to the new position
        character.transform.Translate((targetPosition - character.transform.position) * Time.deltaTime);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (_canJump == true)
        {
            _isJumpPerformed = true;
        }
    }

    private void EvaluateJump()
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

            if (_isJumpPerformed == true)
            {
                _velocity.y = jumpSpeed;
                _isJumping = true;
                _isJumpPerformed = false;
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

        // Compute new position
        Vector3 newLocalPosition = ((PlayerFSM)character).model.localPosition + (character.transform.up * _velocity.y * Time.deltaTime);

        // Clamp model position as it can't be 
        newLocalPosition.y = ((PlayerFSM)character).isGrounded == false
            //below character position while not grounded
            ? Mathf.Max(0f, newLocalPosition.y) 
            //above 0f while grounded
            : Mathf.Min(0f, newLocalPosition.y);

        // Move the controller
        ((PlayerFSM)character).model.localPosition = newLocalPosition;
    }

    private void Dash(InputAction.CallbackContext context)
    {
        // Execute dash if player has the ability and energy
        if ((((PlayerData)character.data).playerAbilitiesMask & (uint)PlayerAbilityEnum.DASH) != 0 
            && ((PlayerData)character.data).ConsumeEnergy(_energyForDash) == true)
        {
            _isDashing = true;

            character.StartCoroutine(EvaluateDash());
        }
    }

    private IEnumerator EvaluateDash()
    {
        // Switch to new camera and animation
        dashCamera.m_Priority = 200;
        character.animator.SetBool(ANIMATION_PARAM_DASH, true);

        yield return new WaitForSeconds(dashDelayIn);

        Vector3 destination = character.transform.position;

        // Give priority to lateral dash
        if (((PlayerFSM)character).target != null && Mathf.Abs(_input.x) >= 0.2f)
        {
            destination += _input.x > 0f ? character.transform.right * dashDistance : character.transform.right * (-dashDistance);
        }
        // Otherwise use forward direction (no input leads to dash backward)
        else
        {
            destination += _input.y > 0f ? character.transform.forward * dashDistance : character.transform.forward * (-dashDistance);
        }

        Vector3 direction = destination - character.transform.position;
        direction.y = 0.0f;

        if (_currentDashFx == null)
        {
            GameObject fx = GamePoolManager.instance.UseFromPool(dashFx.gameObject.name);

            _currentDashFx = fx.GetComponent<ParticleSystem>();
        }

        _currentDashFx.transform.position = character.transform.position;
        _currentDashFx.transform.rotation = Quaternion.LookRotation(direction);

        // Compute fx lifetime based on the dash distance
        ParticleSystem.MainModule main = _currentDashFx.main;
        main.startLifetime = direction.magnitude / _currentDashFx.main.startSpeed.constant;

        _currentDashFx.gameObject.SetActive(true);
        _currentDashFx.Play();

        // Move to the new position
        character.transform.position = destination;

        character.StartCoroutine(OnDashComplete());
    }

    private IEnumerator OnDashComplete()
    {
        yield return new WaitForSeconds(dashDelayOut);

        // Disable dash camera and animation
        dashCamera.m_Priority = 0;
        character.animator.SetBool(ANIMATION_PARAM_DASH, false);

        _isDashing = false;
        _dashTime = Time.time;

        CharacterGameEvent.instance.CompleteDash(character.data.uniqueId, _energyForDash);
    }

    public override void Exit()
    {
        // Reset _input and _velocity before exiting move state
        _input = Vector2.zero;
        _velocity = Vector3.zero;

        // Unbind controls
        if (InputManager.instance != null)
        {
            InputManager.instance.gameplay.Jump.performed -= Jump;
            InputManager.instance.gameplay.Dash.performed -= Dash;
        }

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
using System;
using UnityEngine;

[Serializable]
public class PlayerStateMove : CharacterFSM.State
{
    private const string ANIMATION_PARAM_VELOCTIY_X = "VelocityX";
    private const string ANIMATION_PARAM_VELOCTIY_Y = "VelocityY";

    private Vector3 _input = Vector3.zero;
    private Vector3 _velocity = Vector3.zero;

    public float movementSpeed = 3f;

    public override void Update()
    {
        ((PlayerFSM)owner).Rotate();

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
        _input.x = ((PlayerFSM)owner).target != null ? Input.GetAxis("Horizontal") * modifier : 0f;

        // Apply input amplitude to forward velocity
        _velocity.z = _input.z * movementSpeed;
        // Apply input amplitude to left velocity
        _velocity.x = _input.x * movementSpeed;
    }

    private void Animate()
    {
        ((PlayerFSM)owner).animator.SetFloat(ANIMATION_PARAM_VELOCTIY_X, _input.x);
        ((PlayerFSM)owner).animator.SetFloat(ANIMATION_PARAM_VELOCTIY_Y, _input.z);
    }

    public override void FixedUpdate()
    {
        Vector3 targetPosition = owner.transform.position + _velocity;

        // Move to the new position
        owner.transform.Translate((targetPosition - owner.transform.position) * Time.deltaTime);
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
        UnityEditor.Handles.Label(owner.transform.position, "Velocity : " + _velocity);
    }
#endif
}
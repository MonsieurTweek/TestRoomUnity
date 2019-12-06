﻿using UnityEngine;
using System.Collections;

namespace RPGC_TestRoom_Anims{
	
	public class RPGC_TestRoom_InputController : MonoBehaviour{

		//Inputs.
		[HideInInspector] public bool inputJump;
		[HideInInspector] public bool inputAttackL;
		[HideInInspector] public bool inputAttackR;
		[HideInInspector] public bool inputSwitchUpDown;
		[HideInInspector] public bool inputStrafe;
		[HideInInspector] public float inputAimVertical = 0;
		[HideInInspector] public float inputAimHorizontal = 0;
		[HideInInspector] public float inputHorizontal = 0;
		[HideInInspector] public float inputVertical = 0;
		[HideInInspector] public bool inputRoll;

		//Variables
		[HideInInspector] public bool allowedInput = true;
		[HideInInspector] public Vector3 moveInput;
		[HideInInspector] public Vector2 aimInput;

        private float _rotation = 0f;
        private float _rorationSpeed = 100f;

        RPGC_TestRoom_Controller RPGC_TestRoom_Controller;

        /// <summary>
        /// Input abstraction for easier asset updates using outside control schemes.
        /// </summary>
        void Inputs(){
			inputJump = Input.GetButtonDown("Jump");
			//inputAttackL = Input.GetButtonDown("AttackL");
			//inputAttackR = Input.GetButtonDown("AttackR");
			//inputSwitchUpDown = Input.GetButtonDown("SwitchUpDown");
			//inputStrafe = Input.GetKey(KeyCode.LeftShift);
			//inputAimVertical = Input.GetAxisRaw("AimVertical");
			//inputAimHorizontal = Input.GetAxisRaw("AimHorizontal");
			inputHorizontal = Input.GetAxisRaw("Horizontal");
			inputVertical = Input.GetAxisRaw("Vertical");
			//inputRoll = Input.GetButtonDown("L3");
		}

		void Awake(){
			allowedInput = true;
            RPGC_TestRoom_Controller = GetComponent<RPGC_TestRoom_Controller>();
        }

		void Update(){
			Inputs();
			moveInput = CameraRelativeInput(0f, inputVertical);
            aimInput = new Vector2(inputAimHorizontal, inputAimVertical);

            // Rotation
            _rotation += inputHorizontal * _rorationSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, _rotation, 0);
        }

		/// <summary>
		/// Movement based off camera facing.
		/// </summary>
		Vector3 CameraRelativeInput(float inputX, float inputZ){
			//Forward vector relative to the camera along the x-z plane   
			Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
			forward.y = 0;
			forward = forward.normalized;
			//Right vector relative to the camera always orthogonal to the forward vector.
			Vector3 right = new Vector3(forward.z, 0, -forward.x);
			Vector3 relativeVelocity = inputHorizontal * right + inputVertical * forward;
			//Reduce input for diagonal movement.
			if(relativeVelocity.magnitude > 1){
				relativeVelocity.Normalize();
			}
			return relativeVelocity;
		}

		public bool HasAnyInput(){
			if(allowedInput && moveInput != Vector3.zero && aimInput != Vector2.zero && inputJump != false){
				return true;
			}
			else{
				return false;
			}
		}
		
		public bool HasMoveInput(){
			if(allowedInput && moveInput != Vector3.zero){
				return true;
			}
			else{
				return false;
			}
		}
		
		public bool HasAimInput(){
			if(allowedInput && (aimInput.x < -0.8f || aimInput.x > 0.8f) || (aimInput.y < -0.8f || aimInput.y > 0.8f)){
				return true;
			}
			else{
				return false;
			}
		}
	}
}
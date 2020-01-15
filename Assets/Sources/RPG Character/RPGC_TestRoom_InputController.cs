using UnityEngine;
using System.Collections;
using Cinemachine;

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

        RPGC_TestRoom_Controller RPGC_TestRoom_Controller;

        public CinemachineFreeLook thirdPersonCamera;

        /// <summary>
        /// Input abstraction for easier asset updates using outside control schemes.
        /// </summary>
        void Inputs()
        {
			inputJump = Input.GetButtonDown("Jump");
			inputAttackL = Input.GetButtonDown("AttackL");
			inputAttackR = Input.GetButtonDown("AttackR");
			//inputSwitchUpDown = Input.GetButtonDown("SwitchUpDown");
			//inputStrafe = Input.GetKey(KeyCode.LeftShift);
			inputAimVertical = Input.GetAxisRaw("Mouse Y");
			inputAimHorizontal = Input.GetAxisRaw("Mouse X");
			inputHorizontal = Input.GetAxisRaw("Horizontal");
			inputVertical = Input.GetAxisRaw("Vertical");
			//inputRoll = Input.GetButtonDown("L3");
		}

		void Awake()
        {
			allowedInput = true;
            RPGC_TestRoom_Controller = GetComponent<RPGC_TestRoom_Controller>();
            thirdPersonCamera.m_RecenterToTargetHeading.m_enabled = true;
        }

		void Update()
        {
			Inputs();
            moveInput = CameraRelativeInput(inputHorizontal, inputVertical);
            if(inputVertical < 0)
            {
                thirdPersonCamera.m_RecenterToTargetHeading.m_enabled = false;
            } else
            {
                thirdPersonCamera.m_RecenterToTargetHeading.m_enabled = true;
            }
            aimInput = new Vector2(inputAimHorizontal, inputAimVertical);
        }

        /// <summary>
        /// Movement based off camera facing.
        /// </summary>
        public Vector3 CameraRelativeInput(float inputX, float inputZ)
        {
			//Forward vector relative to the camera along the x-z plane   
			Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
			forward.y = 0;
			forward = forward.normalized;
			//Right vector relative to the camera always orthogonal to the forward vector.
			Vector3 right = new Vector3(forward.z, 0, -forward.x);
			Vector3 relativeVelocity = inputX * right + inputZ * forward;
			//Reduce input for diagonal movement.
			if(relativeVelocity.magnitude > 1)
            {
				relativeVelocity.Normalize();
			}
			return relativeVelocity;
		}

		public bool HasAnyInput(){
			if(allowedInput && moveInput != Vector3.zero && aimInput != Vector2.zero && inputJump != false)
            {
				return true;
			}
			else{
				return false;
			}
        }

        public bool HasMoveInputVertical()
        {
            if (allowedInput && inputVertical != 0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool HasMoveInputHorizontal()
        {
            if (allowedInput && inputHorizontal != 0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool HasMoveInput()
        {
			if(allowedInput && (HasMoveInputHorizontal() || HasMoveInputVertical()))
            {
				return true;
			}
			else{
				return false;
			}
		}
		
		public bool HasAimInput()
        {
			if(allowedInput && (aimInput.x < -0.8f || aimInput.x > 0.8f) || (aimInput.y < -0.8f || aimInput.y > 0.8f))
            {
				return true;
			}
			else{
				return false;
			}
		}
	}
}
using UnityEngine;
using System.Collections;

namespace RPGC_TestRoom_Anims{

	public enum RPGCharacterState{
		Idle = 0,
		Move = 1,
		Jump = 2,
		DoubleJump = 3,
		Fall = 4,
		Block = 6,
		Roll = 8
	}

	public class RPGC_TestRoom_MovementController : SuperStateMachine{
		
		//Components.
		private SuperCharacterController superCharacterController;
		private RPGC_TestRoom_Controller RPGC_TestRoom_Controller;
		[HideInInspector] public UnityEngine.AI.NavMeshAgent navMeshAgent;
		private RPGC_TestRoom_InputController RPGC_TestRoom_InputController;
		private Rigidbody rb;
		private Animator animator;
		public RPGCharacterState rpgCharacterState;

		[HideInInspector] public bool useMeshNav = false;
		[HideInInspector] public Vector3 lookDirection { get; private set; }
		[HideInInspector] public bool isKnockback;
		public float knockbackMultiplier = 1f;

		//Jumping.
		[HideInInspector] public bool canJump;
		[HideInInspector] public bool canDoubleJump = false;
		bool doublejumped = false;
		public float gravity = 25.0f;
		public float jumpAcceleration = 5.0f;
		public float jumpHeight = 3.0f;
		public float doubleJumpHeight = 4f;

		//Movement.
		[HideInInspector] public Vector3 currentVelocity;
		[HideInInspector] public bool isMoving = false;
		[HideInInspector] public bool canMove = true;
		public float movementAcceleration = 90.0f;
		public float walkSpeed = 4f;
		public float runSpeed = 6f;
		public float rotationSpeed = 4.5f;
		public float groundFriction = 50f;

		//Rolling.
		[HideInInspector] public bool isRolling = false;
		public float rollSpeed = 8;
		public float rollduration = 0.35f;
		private int rollNumber;

		//Air control.
		public float inAirSpeed = 6f;

		void Awake(){
			superCharacterController = GetComponent<SuperCharacterController>();
			RPGC_TestRoom_Controller = GetComponent<RPGC_TestRoom_Controller>();
			RPGC_TestRoom_InputController = GetComponent<RPGC_TestRoom_InputController>();
			navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
			animator = GetComponentInChildren<Animator>();
			rb = GetComponent<Rigidbody>();
			if(rb != null){
				//Set restraints on startup if using Rigidbody.
				rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
			}
			//Set currentState to idle on startup.
			currentState = RPGCharacterState.Idle;
			rpgCharacterState = RPGCharacterState.Idle;
		}

		/*
		Update is normally run once on every frame update. We won't be using it in this case, since the SuperCharacterController component sends a callback Update called SuperUpdate. SuperUpdate is recieved by the SuperStateMachine, and then fires further callbacks depending on the state

		void Update(){
		}

		*/

		//Put any code in here you want to run BEFORE the state's update function. This is run regardless of what state you're in.
		protected override void EarlyGlobalSuperUpdate(){
		}

		//Put any code in here you want to run AFTER the state's update function.  This is run regardless of what state you're in.
		protected override void LateGlobalSuperUpdate(){
            //Move the player by our velocity every frame.
            if (currentVelocity.z != 0f || currentVelocity.y != 0f)
            {
                transform.position += currentVelocity * superCharacterController.deltaTime;
            }
			//If using Navmesh nagivation, update values.
            /*
			if(navMeshAgent != null){
				if(useMeshNav){
					if(navMeshAgent.velocity.sqrMagnitude > 0){
						animator.SetBool("Moving", true);
						animator.SetFloat("Velocity Z", navMeshAgent.velocity.magnitude);
					}
					else{
						animator.SetFloat("Velocity Z", 0);
					}
				}
			}
            */
			//If alive and is moving, set animator.
			if(!useMeshNav && !RPGC_TestRoom_Controller.isDead && canMove){
				if(currentVelocity.magnitude > 0 && RPGC_TestRoom_InputController.HasMoveInput())
                {
                    isMoving = true;
                    animator.SetBool("Moving", true);
                    animator.SetFloat("Velocity X", RPGC_TestRoom_InputController.inputHorizontal);
                    animator.SetFloat("Velocity Z", RPGC_TestRoom_InputController.inputVertical);
                }
				else{
					isMoving = false;
					animator.SetBool("Moving", false);
                    animator.SetFloat("Velocity X", 0f);
                    animator.SetFloat("Velocity Z", 0f);
                }
			}
			//Strafing.
			if(!RPGC_TestRoom_Controller.isStrafing){
				RotateTowardsMovementDir();
			}
			else if(RPGC_TestRoom_Controller.target != null) {
				Strafing(RPGC_TestRoom_Controller.target.transform.position);
			}

            // Security to avoid goind under the map
            if(gameObject.transform.position.y < -5f && RPGC_TestRoom_Controller.isDead == false)
            {
                RPGC_TestRoom_Controller.Death();
            } else if(gameObject.transform.position.y < -10f && RPGC_TestRoom_Controller.isDead == true)
            {
                gameObject.transform.position = Vector3.zero;
                RPGC_TestRoom_Controller.Revive();
            }
        }

		private bool AcquiringGround(){
			return superCharacterController.currentGround.IsGrounded(false, 0.01f);
		}

		public bool MaintainingGround(){
			return superCharacterController.currentGround.IsGrounded(true, 0.5f);
		}

		public void RotateGravity(Vector3 up){
			lookDirection = Quaternion.FromToRotation(transform.up, up) * lookDirection;
		}

		/// <summary>
		/// Constructs a vector representing our movement local to our lookDirection, which is controlled by the camera.
		/// </summary>
		private Vector3 LocalMovement(){
			return RPGC_TestRoom_InputController.moveInput;
		}

		// Calculate the initial velocity of a jump based off gravity and desired maximum height attained
		private float CalculateJumpSpeed(float jumpHeight, float gravity){
			return Mathf.Sqrt(2 * jumpHeight * gravity);
		}

		//Below are the state functions. Each one is called based on the name of the state, so when currentState = Idle, we call Idle_EnterState. If currentState = Jump, we call Jump_SuperUpdate()
		void Idle_EnterState(){
			superCharacterController.EnableSlopeLimit();
			superCharacterController.EnableClamping();
			canJump = true;
			doublejumped = false;
			canDoubleJump = false;
			animator.SetInteger("Jumping", 0);
		}

		//Run every frame we are in the idle state.
		void Idle_SuperUpdate(){
			//If Jump.
			if(RPGC_TestRoom_InputController.allowedInput && RPGC_TestRoom_InputController.inputJump){
				currentState = RPGCharacterState.Jump;
				rpgCharacterState = RPGCharacterState.Jump;
				return;
			}
			if(!MaintainingGround()){
				currentState = RPGCharacterState.Fall;
				rpgCharacterState = RPGCharacterState.Fall;
				return;
			}
			if(RPGC_TestRoom_InputController.HasMoveInput() && canMove){
				currentState = RPGCharacterState.Move;
				rpgCharacterState = RPGCharacterState.Move;
				return;
			}
			//Apply friction to slow to a halt.
			currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, groundFriction * superCharacterController.deltaTime);
        }

		void Idle_ExitState(){
			//Run once when exit the idle state.
		}

		void Move_SuperUpdate(){
			//If Jump.
			if(RPGC_TestRoom_InputController.allowedInput && RPGC_TestRoom_InputController.inputJump){
				currentState = RPGCharacterState.Jump;
				rpgCharacterState = RPGCharacterState.Jump;
				return;
			}
			if(!MaintainingGround()){
				currentState = RPGCharacterState.Fall;
				rpgCharacterState = RPGCharacterState.Fall;
				return;
			}
			//Set speed determined by movement type.
			if(RPGC_TestRoom_InputController.HasMoveInput() && canMove){
                // Keep strafing animations from playing when the player is moving forward or backward
                /*if(RPGC_TestRoom_InputController.HasMoveInputVertical())
                {
                    animator.SetFloat("Velocity X", 0f);
                }*/
				//Strafing or Walking.
				if(RPGC_TestRoom_Controller.isStrafing){
					currentVelocity = Vector3.MoveTowards(currentVelocity, LocalMovement() * walkSpeed, movementAcceleration * superCharacterController.deltaTime);
					if(RPGC_TestRoom_Controller.weapon != Weapon.RELAX && RPGC_TestRoom_Controller.target != null){
						Strafing(RPGC_TestRoom_Controller.target.transform.position);
					}
					return;
				}
                //Run.
                float currentSpeed = runSpeed;
                if(RPGC_TestRoom_InputController.inputVertical <= 0.5f)
                {
                    currentSpeed = walkSpeed;
                }
                // Target is locked if we are strafing
                Vector3 target = LocalMovement() * currentSpeed;
                currentVelocity = Vector3.MoveTowards(currentVelocity, target, movementAcceleration * superCharacterController.deltaTime);
            }
			else{
				currentState = RPGCharacterState.Idle;
				rpgCharacterState = RPGCharacterState.Idle;
				return;
			}
		}

		void Jump_EnterState(){
			superCharacterController.DisableClamping();
			superCharacterController.DisableSlopeLimit();
			currentVelocity += superCharacterController.up * CalculateJumpSpeed(jumpHeight, gravity);
			//Set weaponstate to Unarmed if Relaxed.
			if(RPGC_TestRoom_Controller.weapon == Weapon.RELAX){
				RPGC_TestRoom_Controller.weapon = Weapon.UNARMED;
				animator.SetInteger("Weapon", 0);
			}
			canJump = false;
			animator.SetInteger("Jumping", 1);
			animator.SetTrigger("JumpTrigger");
		}

		void Jump_SuperUpdate(){
			Vector3 planarMoveDirection = Math3d.ProjectVectorOnPlane(superCharacterController.up, currentVelocity);
			Vector3 verticalMoveDirection = currentVelocity - planarMoveDirection;
			if(Vector3.Angle(verticalMoveDirection, superCharacterController.up) > 90 && AcquiringGround()){
				currentVelocity = planarMoveDirection;
				currentState = RPGCharacterState.Idle;
				rpgCharacterState = RPGCharacterState.Idle;
				return;            
			}
			planarMoveDirection = Vector3.MoveTowards(planarMoveDirection, LocalMovement() * inAirSpeed, jumpAcceleration * superCharacterController.deltaTime);
			verticalMoveDirection -= superCharacterController.up * gravity * superCharacterController.deltaTime;
			currentVelocity = planarMoveDirection + verticalMoveDirection;
			//Can double jump if starting to fall.
			if(currentVelocity.y < 0){
				DoubleJump();
			}
		}

		void DoubleJump_EnterState(){
            RotateTowardsMovementDir();
			currentVelocity += superCharacterController.up * CalculateJumpSpeed(doubleJumpHeight, gravity);
			canDoubleJump = false;
			doublejumped = true;
			animator.SetInteger("Jumping", 3);
			animator.SetTrigger("JumpTrigger");
		}

		void DoubleJump_SuperUpdate(){
			Jump_SuperUpdate();
		}

		void DoubleJump(){
			if(!doublejumped){
				canDoubleJump = true;
			}
            // Too close from the ground
            if(superCharacterController.currentGround.IsGrounded(false, 1f))
            {
                canDoubleJump = false;
            }
			if (RPGC_TestRoom_InputController.inputJump && canDoubleJump && !doublejumped){
				currentState = RPGCharacterState.DoubleJump;
				rpgCharacterState = RPGCharacterState.DoubleJump;
			}
		}

		void Fall_EnterState(){
			if(!doublejumped){
				canDoubleJump = true;
			}
			superCharacterController.DisableClamping();
			superCharacterController.DisableSlopeLimit();
			canJump = false;
			animator.SetInteger("Jumping", 2);
			animator.SetTrigger("JumpTrigger");
		}

		void Fall_SuperUpdate(){
			if(AcquiringGround()){
				currentVelocity = Math3d.ProjectVectorOnPlane(superCharacterController.up, currentVelocity);
				currentState = RPGCharacterState.Idle;
				rpgCharacterState = RPGCharacterState.Idle;
				return;
			}
			DoubleJump();
			currentVelocity -= superCharacterController.up * gravity * superCharacterController.deltaTime;
		}

		void Roll_SuperUpdate(){
			if(rollNumber == 1){
				currentVelocity = Vector3.MoveTowards(currentVelocity, transform.forward * rollSpeed, groundFriction * superCharacterController.deltaTime);
			}
			if(rollNumber == 2){
				currentVelocity = Vector3.MoveTowards(currentVelocity, transform.right * rollSpeed, groundFriction * superCharacterController.deltaTime);
			}
			if(rollNumber == 3){
				currentVelocity = Vector3.MoveTowards(currentVelocity, -transform.forward * rollSpeed, groundFriction * superCharacterController.deltaTime);
			}
			if(rollNumber == 4){
				currentVelocity = Vector3.MoveTowards(currentVelocity, -transform.right * rollSpeed, groundFriction * superCharacterController.deltaTime);
			}
		}

		public void DirectionalRoll(){
			//Check which way the dash is pressed relative to the character facing.
			float angle = Vector3.Angle(RPGC_TestRoom_InputController.moveInput, -transform.forward);
			float sign = Mathf.Sign(Vector3.Dot(transform.up, Vector3.Cross(RPGC_TestRoom_InputController.aimInput, transform.forward)));
			//Angle in [-179,180].
			float signed_angle = angle * sign;
			//Angle in 0-360.
			float angle360 = (signed_angle + 180) % 360;
			//Deternime the animation to play based on the angle.
			if(angle360 > 315 || angle360 < 45){
				StartCoroutine(_Roll(1));
			}
			if(angle360 > 45 && angle360 < 135){
				StartCoroutine(_Roll(2));
			}
			if(angle360 > 135 && angle360 < 225){
				StartCoroutine(_Roll(3));
			}
			if(angle360 > 225 && angle360 < 315){
				StartCoroutine(_Roll(4));
			}
		}

		/// <summary>
		/// Character Roll.
		/// </summary>
		/// <param name="1">Forward.</param>
		/// <param name="2">Right.</param>
		/// <param name="3">Backward.</param>
		/// <param name="4">Left.</param>
		public IEnumerator _Roll(int roll){
			rollNumber = roll;
			currentState = RPGCharacterState.Roll;
			rpgCharacterState = RPGCharacterState.Roll;
			if(RPGC_TestRoom_Controller.weapon == Weapon.RELAX){
				RPGC_TestRoom_Controller.weapon = Weapon.UNARMED;
				animator.SetInteger("Weapon", 0);
			}
			animator.SetInteger("Action", rollNumber);
			animator.SetTrigger("RollTrigger");
			isRolling = true;
			RPGC_TestRoom_Controller.canAction = false;
			yield return new WaitForSeconds(rollduration);
			isRolling = false;
			RPGC_TestRoom_Controller.canAction = true;
			currentState = RPGCharacterState.Idle;
			rpgCharacterState = RPGCharacterState.Idle;
		}

		public void SwitchCollisionOff(){
			canMove = false;
			superCharacterController.enabled = false;
			animator.applyRootMotion = true;
			if(rb != null){
				rb.isKinematic = false;
			}
		}

		public void SwitchCollisionOn(){
			canMove = true;
			superCharacterController.enabled = true;
			animator.applyRootMotion = false;
			if(rb != null){
				rb.isKinematic = true;
			}
		}

		void RotateTowardsMovementDir(){
            if (currentVelocity != Vector3.zero)
            {
                if (RPGC_TestRoom_InputController.inputVertical > 0f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(new Vector3(currentVelocity.x, 0f, currentVelocity.z));
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                } else if (RPGC_TestRoom_InputController.inputVertical < 0f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(new Vector3(currentVelocity.x, 0f, currentVelocity.z) * -1);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                }
            }
		}

		void RotateTowardsTarget(Vector3 targetPosition){
			Quaternion targetRotation = Quaternion.LookRotation(targetPosition - new Vector3(transform.position.x, 0, transform.position.z));
			transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, (rotationSpeed * Time.deltaTime) * rotationSpeed);
		}

		void Aiming(){
			for(int i = 0; i < Input.GetJoystickNames().Length; i++){
				//If right joystick is moved, use that for facing.
				if(Mathf.Abs(RPGC_TestRoom_InputController.inputAimHorizontal) > 0.8 || Mathf.Abs(RPGC_TestRoom_InputController.inputAimVertical) < -0.8){
					Vector3 joyDirection = new Vector3(RPGC_TestRoom_InputController.inputAimHorizontal, 0, -RPGC_TestRoom_InputController.inputAimVertical);
					joyDirection = joyDirection.normalized;
					Quaternion joyRotation = Quaternion.LookRotation(joyDirection);
					transform.rotation = joyRotation;
				}
			}
			//No joysticks, use mouse aim.
			if(Input.GetJoystickNames().Length == 0){
				Plane characterPlane = new Plane(Vector3.up, transform.position);
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				Vector3 mousePosition = new Vector3(0, 0, 0);
				float hitdist = 0.0f;
				if(characterPlane.Raycast(ray, out hitdist)){
					mousePosition = ray.GetPoint(hitdist);
				}
				mousePosition = new Vector3(mousePosition.x, transform.position.y, mousePosition.z);
				RotateTowardsTarget(mousePosition);
			}
			//Update animator with local movement values.
			animator.SetFloat("Velocity X", transform.InverseTransformDirection(currentVelocity).x);
			animator.SetFloat("Velocity Z", transform.InverseTransformDirection(currentVelocity).z);
		}

		//Update animator with local movement values.
		void Strafing(Vector3 targetPosition){
			animator.SetFloat("Velocity X", transform.InverseTransformDirection(currentVelocity).x);
			animator.SetFloat("Velocity Z", transform.InverseTransformDirection(currentVelocity).z);
			RotateTowardsTarget(targetPosition);
		}

		public IEnumerator _Knockback(Vector3 knockDirection, int knockBackAmount, int variableAmount){
			isKnockback = true;
			StartCoroutine(_KnockbackForce(knockDirection, knockBackAmount, variableAmount));
			yield return new WaitForSeconds(.1f);
			isKnockback = false;
		}

		IEnumerator _KnockbackForce(Vector3 knockDirection, int knockBackAmount, int variableAmount){
			while(isKnockback){
				rb.AddForce(knockDirection * ((knockBackAmount + Random.Range(-variableAmount, variableAmount)) * (knockbackMultiplier * 10)), ForceMode.Impulse);
				yield return null;
			}
		}

		//Keep character from moving.
		public void LockMovement(){
			canMove = false;
			animator.SetBool("Moving", false);
			animator.applyRootMotion = true;
			currentVelocity = new Vector3(0, 0, 0);
		}

		public void UnlockMovement(){
			canMove = true;
			animator.applyRootMotion = false;
		}
	}
}
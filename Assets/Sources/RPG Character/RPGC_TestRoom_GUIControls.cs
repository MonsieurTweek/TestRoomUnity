using UnityEngine;
using System.Collections;

namespace RPGC_TestRoom_Anims{

	public class RPGC_TestRoom_GUIControls : MonoBehaviour{
		RPGC_TestRoom_Controller RPGC_TestRoom_Controller;
		RPGC_TestRoom_MovementController RPGC_TestRoom_MovementController;
		public bool useNavAgent;

		void Awake(){
			RPGC_TestRoom_Controller = GetComponent<RPGC_TestRoom_Controller>();
			RPGC_TestRoom_MovementController = GetComponent<RPGC_TestRoom_MovementController>();
		}

		void OnGUI(){
			//General.
			if(!RPGC_TestRoom_Controller.isDead){
				//Actions.
				if(RPGC_TestRoom_Controller.canAction){
					if(RPGC_TestRoom_MovementController.MaintainingGround()){
						//Use NavMesh.
						useNavAgent = GUI.Toggle(new Rect(500, 15, 100, 30), useNavAgent, "Use NavAgent");
						if(useNavAgent && RPGC_TestRoom_MovementController.navMeshAgent != null){
							RPGC_TestRoom_MovementController.useMeshNav = true;
							RPGC_TestRoom_MovementController.navMeshAgent.enabled = true;
						}
						else{
							RPGC_TestRoom_MovementController.useMeshNav = false;
							RPGC_TestRoom_MovementController.navMeshAgent.enabled = false;
						}
						//Rolling.
						if(GUI.Button(new Rect(25, 15, 100, 30), "Roll Forward")){
							StartCoroutine(RPGC_TestRoom_MovementController._Roll(1));
						}
						if(GUI.Button(new Rect(130, 15, 100, 30), "Roll Backward")){
							StartCoroutine(RPGC_TestRoom_MovementController._Roll(3));
						}
						if(GUI.Button(new Rect(25, 45, 100, 30), "Roll Left")){
							StartCoroutine(RPGC_TestRoom_MovementController._Roll(4));
						}
						if(GUI.Button(new Rect(130, 45, 100, 30), "Roll Right")){
							StartCoroutine(RPGC_TestRoom_MovementController._Roll(2));
						}
						//Turning.
						if(GUI.Button(new Rect(340, 15, 100, 30), "Turn Left")){
							StartCoroutine(RPGC_TestRoom_Controller._Turning(1));
						}
						if(GUI.Button(new Rect(340, 45, 100, 30), "Turn Right")){
							StartCoroutine(RPGC_TestRoom_Controller._Turning(2));
						}
						//ATTACK LEFT.
						if(GUI.Button(new Rect(25, 85, 100, 30), "Attack L")){
							RPGC_TestRoom_Controller.Attack(1);
						}
						//ATTACK RIGHT.
						if(GUI.Button(new Rect(130, 85, 100, 30), "Attack R")){
							RPGC_TestRoom_Controller.Attack(2);
						}
						//Kicking.
						if(GUI.Button(new Rect(25, 115, 100, 30), "Left Kick")){
							RPGC_TestRoom_Controller.AttackKick(1);
						}
						if(GUI.Button(new Rect(130, 115, 100, 30), "Right Kick")){
							RPGC_TestRoom_Controller.AttackKick(3);
						}
						if(GUI.Button(new Rect(30, 240, 100, 30), "Get Hit")){
							RPGC_TestRoom_Controller.GetHit();
						}
					}
					//Jump / Double Jump.
					if((RPGC_TestRoom_MovementController.canJump || RPGC_TestRoom_MovementController.canDoubleJump) && RPGC_TestRoom_Controller.canAction){
						if(RPGC_TestRoom_MovementController.MaintainingGround()){
							if(GUI.Button(new Rect(25, 175, 100, 30), "Jump")){
								if(RPGC_TestRoom_MovementController.canJump){
									RPGC_TestRoom_MovementController.currentState = RPGCharacterState.Jump;
									RPGC_TestRoom_MovementController.rpgCharacterState = RPGCharacterState.Jump;
								}
							}
						}
						if(RPGC_TestRoom_MovementController.canDoubleJump){
							if(GUI.Button(new Rect(25, 175, 100, 30), "Jump Flip")){
								RPGC_TestRoom_MovementController.currentState = RPGCharacterState.DoubleJump;
								RPGC_TestRoom_MovementController.rpgCharacterState = RPGCharacterState.DoubleJump;
							}
						}
					}
					//Death.
					if(RPGC_TestRoom_MovementController.MaintainingGround() && RPGC_TestRoom_Controller.canAction){
						if(GUI.Button(new Rect(30, 270, 100, 30), "Death")){
							RPGC_TestRoom_Controller.Death();
						}
					}
				}
			}
			//Dead
			else{
				//Death.
				if(GUI.Button(new Rect(30, 270, 100, 30), "Revive")){
					RPGC_TestRoom_Controller.Revive();
				}
			}
		}
	}
}
using UnityEngine;
using System.Collections;

public partial class Humanoid_CharacterController : MonoBehaviour {

	public void Move(Vector3 moveVector)
	{
		if (DEBUG) Debug.Log ("CharacterController.move vector3: " + moveVector);
		if (character_Controller.isGrounded) {
			/* 
			if (move.magnitude > 1f) move.Normalize ();
			move = transform.InverseTransformDirection (move);
			float turnAmount = Mathf.Atan2 (move.x, move.z); 
			float forwardAmount = move.z;

			transform.Rotate (0, turnAmount * turnSpeed * Time.deltaTime, 0); 

			character_Animator.SetFloat ("Forward", forwardAmount, 0f, Time.deltaTime);
			//character_Animator.applyRootMotion = true;

			Vector3 movementVector = (character_Animator.deltaPosition * movementSpeed) / Time.deltaTime;
			movementVector.y = character_Rigibody.velocity.y;
			//character_Rigibody.velocity = movementVector; */
			//float forwardAmount = moveVector.z;
			character_Animator.SetFloat ("Forward", moveVector.magnitude, 0f, Time.deltaTime);
			
			//moveVector = transform.TransformDirection(moveVector);
			//may handle differently 
			if(sprinting) moveVector *= character_SprintSpeed;
			else moveVector *= character_NormalSpeed;
		} else {
			if (DEBUG) Debug.Log ("CharacterController - Move: Airborne");
			//handleAirborneMotion ();
		}
		moveVector.y -= character_Gravity + Time.deltaTime;
		character_Controller.Move(moveVector * Time.deltaTime);
	}
}
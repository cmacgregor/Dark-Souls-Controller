using UnityEngine;
using System.Collections;

public partial class Humanoid_CharacterController : MonoBehaviour {
	
	public void Move(Vector3 moveVector)
	{
		//if (DEBUG) Debug.Log ("CharacterController.move vector3: " + moveVector);

		if (character_Controller.isGrounded)
        {
            moveVector = handleGroundedLocomotion(moveVector);
        }
        else
        {
            handleAirborneLocomotion();
        }

    }

    private Vector3 handleGroundedLocomotion(Vector3 moveVector)
    {
        character_Animator.SetBool("Falling", false);
        if (moveVector.magnitude > 1) moveVector = Vector3.Normalize(moveVector);
        if (moveVector.x != 0 || moveVector.z != 0)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x,
                                                    Camera.main.transform.eulerAngles.y,
                                                    transform.eulerAngles.z);
        }

        moveVector = transform.TransformDirection(moveVector);
        character_Animator.SetFloat("Forward", moveVector.magnitude, 0f, Time.deltaTime);
        if (moveVector.x != 0 || moveVector.z != 0) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveVector), 500f);
        if (sprinting) moveVector *= character_SprintSpeed;

        //Apply Character Weight Modifier to movement Vector 
        moveVector *= character_WeightSpeedMultiplier;
        moveVector *= Time.deltaTime;
        character_Controller.Move(moveVector);
        return moveVector;
    }

    private void handleAirborneLocomotion()
    {
        //if (DEBUG) Debug.Log ("CharacterController - Move: Airborne");
        character_Animator.SetBool("Falling", true);
        character_Controller.Move(new Vector3(0, -Gravity, 0) * Time.deltaTime);
    }
}
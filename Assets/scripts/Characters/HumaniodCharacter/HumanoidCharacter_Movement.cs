using UnityEngine;
using System.Collections;

namespace HumanoidCharacterClass
{
    public partial class HumanoidCharacter : OverworldCharacter
    {
        public void Move(Vector3 moveVector)
        {
            //if (DEBUG) Debug.Log ("CharacterController.move vector3: " + moveVector);

            if (characterController.isGrounded)
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
            animationTree.SetBool("Falling", false);
            if (moveVector.magnitude > 1) moveVector = Vector3.Normalize(moveVector);
            if (moveVector.x != 0 || moveVector.z != 0)
            {
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x,
                                                        Camera.main.transform.eulerAngles.y,
                                                        transform.eulerAngles.z);
            }

            moveVector = transform.TransformDirection(moveVector);
            animationTree.SetFloat("Forward", moveVector.magnitude, 0f, Time.deltaTime);
            if (moveVector.x != 0 || moveVector.z != 0) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveVector), 500f);
            if (sprinting) moveVector *= sprintSpeed;

            //Apply Character Weight Modifier to movement Vector 
            moveVector *= weightSpeedMultiplier;
            moveVector *= Time.deltaTime;
            characterController.Move(moveVector);
            return moveVector;
        }

        private void handleAirborneLocomotion()
        {
            //if (DEBUG) Debug.Log ("CharacterController - Move: Airborne");
            animationTree.SetBool("Falling", true);
            characterController.Move(new Vector3(0, -Gravity, 0) * Time.deltaTime);
        }

        // -Dodge
        public void dodge()
        {
            if (DEBUG) Debug.Log(this.name + " dodged");
            //if stamina allows 
            if (stamina > 0)
            {
                animationTree.SetTrigger("Dodge");
                //turn collisions off 
            }
        }
    }
}

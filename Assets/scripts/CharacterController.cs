using UnityEngine;
using System.Collections;

namespace CharacterController.Player
{
	public class CharacterController : MonoBehaviour {

		public float movementSpeed = 1f;

		Rigidbody c_Rigibody;
		Animator c_Animator; 

		void Start () {
			//Setup components 
			c_Rigibody = GetComponent<Rigidbody>();
			c_Animator = GetComponent<Animator>();

			c_Rigibody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		}

		public void Move(Vector3 move)
		{
			if (move.magnitude > 1f)
				move.Normalize ();
			move = transform.InverseTransformDirection (move);
			float turnAmount = Mathf.Atan2 (move.x, move.z); 
			float forwardAmount = move.z;

			float turnSpeed = Mathf.Lerp (360, 360, forwardAmount);
			transform.Rotate (0, turnAmount * turnSpeed * Time.deltaTime, 0); 

			c_Animator.SetFloat ("Forward", forwardAmount, 0.1f, Time.deltaTime);
			//c_Animator.applyRootMotion = true;

			Vector3 movementVector = (c_Animator.deltaPosition * movementSpeed) / Time.deltaTime;

			movementVector.y = c_Rigibody.velocity.y;
			c_Rigibody.velocity = movementVector;
		}
	}
}
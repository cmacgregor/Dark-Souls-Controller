using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	//debug variable
//	static bool DEBUG = true;
	static bool DEBUG = false;

	public float movementSpeed = 1f;
	public float turnSpeed = 1000;

	private int weapon_stance;
	private bool sprinting;
	private bool targeting_enemy;
	private bool isGrounded;
	private float character_GravityMultiplier = 2f;
	private float character_GroundDistance = 0.2f;
	private float character_GroundDistanceOriginal;

	Rigidbody character_Rigibody;
	Animator character_Animator; 
	//Animator leftSide_Animator;
	//Animator rightSide_Animator;

	void Start () {
		//Setup components 
		character_Rigibody = GetComponent<Rigidbody>();
		character_Animator = GetComponent<Animator>();

		character_Rigibody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		sprinting = false;
		checkIfGrounded();
	}

	public void Move(Vector3 move)
	{
		if (DEBUG) Debug.Log ("CharacterController.move vector3: " + move);
		checkIfGrounded();
		if (isGrounded) {
			if (sprinting) {

			} else if (targeting_enemy) {

			} else {
				if (move.magnitude > 1f) move.Normalize ();
				move = transform.InverseTransformDirection (move);
				float turnAmount = Mathf.Atan2 (move.x, move.z); 
				float forwardAmount = move.z;

				transform.Rotate (0, turnAmount * turnSpeed * Time.deltaTime, 0); 

				character_Animator.SetFloat ("Forward", forwardAmount, 0f, Time.deltaTime);
				character_Animator.applyRootMotion = true;

				Vector3 movementVector = (character_Animator.deltaPosition * movementSpeed) / Time.deltaTime;
				movementVector.y = character_Rigibody.velocity.y;
				character_Rigibody.velocity = movementVector;
			}
		}
		else {
			if (DEBUG) Debug.Log ("CharacterController - Move: Airborne");
			handleAirborneMotion ();
		}
	}

	void handleAirborneMotion()
	{	
		Debug.Log ("handeling airborne  motion");
		//play falling animation
		character_Animator.SetBool("Falling", true);
		//mitigate forward speed
		// apply extra gravity from multiplier:
		Vector3 drag = (Physics.gravity * character_GravityMultiplier) - Physics.gravity;
		character_Rigibody.AddForce(drag);

		character_GroundDistance = character_Rigibody.velocity.y < 0 ? character_GroundDistanceOriginal : 0.01f;
	}
		
	public void attack(int attack_type)
	{
		//0 = Right Handed Light action
		//1 = Left Handed Light Action
		//2 = Right Handed Heavy Action
		//3 = Left Handed Heavy Action
		//4 = Kick
		if (attack_type == 0) {
			if (DEBUG) Debug.Log ("CharacterController.attack = right handed light action");
		}
		else if (attack_type == 1) {
			if (DEBUG) Debug.Log ("CharacterController.attack = left handed light action");
			character_Animator.SetLayerWeight(1, 1.0f);
			character_Animator.SetTrigger ("LightAction");
		}
		else if (attack_type == 2) {
			if (DEBUG) Debug.Log ("CharacterController.attack = right handed heavy action");
		}
		else if (attack_type == 3) {
			if (DEBUG) Debug.Log ("CharacterController.attack = left handed heavy action");
			character_Animator.SetLayerWeight(1, 1.0f);
			character_Animator.SetTrigger ("HeavyAction");
		}
		else if (attack_type == 5) {
			if (DEBUG) Debug.Log ("CharacterController.attack = kick");
		}
	}

	public void dodge()
	{
		if(DEBUG) Debug.Log ("CharacterController.dodge");
		//if stamina allows 
		if (true) {
//			character_Animator.SetLayerWeight (0, 0f);
//			character_Animator.SetLayerWeight (1, 1f);
//			character_Animator.SetLayerWeight (1, 0f);
//			character_Animator.SetLayerWeight (0, 1f);
			character_Animator.SetTrigger("Dodge");
		}
	}

	public void use_item()
	{
		if(DEBUG) Debug.Log ("CharacterController.use_item");
	}

	public void interact()
	{
		if(DEBUG) Debug.Log ("CharacterController.interact");
	}

	private void checkIfGrounded()
	{
		RaycastHit hitInfo;
		#if UNITY_EDITOR
			// helper to visualise the ground check ray in the scene view
			Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * character_GroundDistance));
		#endif
		// 0.1f is a small offset to start the ray from inside the character
		// it is also good to note that the transform position in the sample assets is at the base of the character
		if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, character_GroundDistance))
		{
			//m_GroundNormal = hitInfo.normal;
			isGrounded = true;
			character_Animator.applyRootMotion = true;
		}
		else
		{
			isGrounded = false;
			//m_GroundNormal = Vector3.up;
			character_Animator.applyRootMotion = false;
		}
	}

	#region property getters and setters
	public int Weapon_stance {
		get {
			return weapon_stance;
		}
		set {
			//Accept any int for now but should be curbed to stances allowed by equipped weapon
			weapon_stance = value;
			if(DEBUG) Debug.Log ("weapon_stance:" + weapon_stance);
		}
	}

	public bool Sprinting {
		get {
			return sprinting;
		}
		set {
			sprinting = value;
		}
	}
		
	public bool Targeting_enemy {
		get {
			return targeting_enemy;
		}
		set {
			targeting_enemy = value;
		}
	}
	#endregion
}
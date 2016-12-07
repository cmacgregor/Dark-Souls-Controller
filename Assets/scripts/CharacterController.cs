using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	//debug variable
	static bool DEBUG = true;
//	static bool DEBUG = false;

	public float movementSpeed = 1f;
	public float turnSpeed = 1000;

	private int weapon_stance;
	private bool sprinting;
	private bool targeting_enemy;
	private bool isGrounded;

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
		isGrounded = checkIfGrounded();

	}

	public void Move(Vector3 move)
	{
		if (DEBUG) Debug.Log ("CharacterController.move vector3: " + move);

		if (isGrounded) {
			//if(DEBUG) Debug.Log("CharacterController is grounded");
			if (sprinting) {

			} else if (targeting_enemy) {

			} else {
				if (move.magnitude > 1f) move.Normalize ();
				move = transform.InverseTransformDirection (move);
				float turnAmount = Mathf.Atan2 (move.x, move.z); 
				float forwardAmount = move.z;

				transform.Rotate (0, turnAmount * turnSpeed * Time.deltaTime, 0); 

				character_Animator.SetFloat ("Forward", forwardAmount, 0.1f, Time.deltaTime);
				//character_Animator.applyRootMotion = true;

				//Vector3 movementVector = (character_Animator.deltaPosition * movementSpeed) / Time.deltaTime;
				//movementVector.y = character_Rigibody.velocity.y;
				//character_Rigibody.velocity = movementVector;
			}
		}
		else {
			//play falling animation
		}
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
		if(true)
		{
			// -sprint dodge
			if (sprinting) 
			{
				if(DEBUG) Debug.Log ("CharacterController.dodge: sprinting Dodge");
				//if directional input is being given roll 

				//else just land

			}
			// -regular roll 
			else
			{
				if(DEBUG) Debug.Log ("player_chacrater.dodge: Regular Dodge");
			}
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

	public bool checkIfGrounded()
	{
		//raycast to check if characer is grounded 
		return true;
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
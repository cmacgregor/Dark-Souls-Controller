using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	//debug variable
	static bool DEBUG = true;

	public float movementSpeed = 1f;

	private int weapon_stance;
	private bool sprinting;

	Rigidbody c_Rigibody;
	Animator c_Animator; 

	void Start () {
		//Setup components 
		c_Rigibody = GetComponent<Rigidbody>();
		c_Animator = GetComponent<Animator>();

		c_Rigibody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		sprinting = false;
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

	public void attack(string attack_type)
	{
		if(DEBUG) Debug.Log ("player_character.attack = " + attack_type);
	}

	public void dodge()
	{
		if(DEBUG) Debug.Log ("player_character.dodge");
		//if stamina allows 
		if(true)
		{
			// -sprint dodge
			if (sprinting) 
			{
				if(DEBUG) Debug.Log ("player_character.dodge: sprinting Dodge");
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
		if(DEBUG) Debug.Log ("player_character.use_item");
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
	#endregion
}
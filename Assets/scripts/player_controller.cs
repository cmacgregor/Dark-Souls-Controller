using UnityEngine;
using System.Collections;

public class player_controller : MonoBehaviour {

	//NEED TO: delegate these calls to external interfaces
	//public OverWorld_Character character;
	[SerializeField] float m_MovingTurnSpeed = 360;
	[SerializeField] float m_StationaryTurnSpeed = 180;
	[Range(1f, 4f)][SerializeField] float m_GravityMultiplier = 2f;
	[SerializeField] float m_MoveSpeedMultiplier = 1f;
	float m_TurnAmount;
	float m_ForwardAmount;

	private Rigidbody rigidBody;
	private Animator anim;
	private Collider character_collider;

	private Transform main_Camera;                  // A reference to the main camera in the scenes transform
	private Vector3 camera_ForwardVector;             // The current forward direction of the camera
	private Vector3 move_Vector;

	public float motionValue;

	public float input_h;
	public float input_v;
	//Attack Inputs
	//Right Hand 
//	bool input_rla;
//	bool input_rha;
//	//Left Hand
//	bool input_lla;
//	bool input_lha;
//	//Action Inputs
	bool input_item;
	bool input_togglestance; 
	bool input_interact;
	bool input_dodge;
	bool input_sprint;

	// Use this for initialization
	void Start () {
		//get Character instance Components 
		rigidBody = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		character_collider = GetComponent<CapsuleCollider>();
		
		//probably create character with equipped armor and weapons 
		
	}

	//Called once per frame
	void Update()
	{
		//if our local state has changed
			//tell server we need to sync our action with networked players
			
		//may need to handle friction
	}
	
	//FixedUpdate is called consistently so we will use this over update to have consistency even at unstable frame rates
	void FixedUpdate () {
		#region Input Variables 
		//Movement Inputs
		input_h = Input.GetAxis ("Horizontal");
		input_v = Input.GetAxis ("Vertical");
		//Attack Inputs
		//Right Hand 
		/*
		input_rla = Input.GetButtonDown("Right Hand Light Attack");
		input_rha = Input.GetButtonDown("Right Hand Heavy Attack");
		//Left Hand
		input_lla = Input.GetButtonDown("Left Hand Light Attack");
		input_lha = Input.GetButtonDown("Left Hand Heavy Attack");
		*/
		//Action Inputs
		input_item = Input.GetButtonDown("Use Item"); 
		input_togglestance = Input.GetButton("Toggle Stance"); //animation mask
		input_interact = Input.GetButtonDown("Interact");
		input_dodge = Input.GetButtonDown("Dodge");
		input_sprint = Input.GetButton("Sprint");
		#endregion

		//storeDir = cam.right;
		
		//bend character model at the waste according to camera's height
		//bendModelBasedUponCameraHeight(cam.height);
		
		//should consider switch statement 
		//if stance toggle button is pushed and we aren't attacking 
		if(input_togglestance)
		{
			//change weapon stance
			Debug.Log("Changing Weapon Stance");
		}
		//if an action is performed that we can't move during 
		//Dodging 
		if(input_dodge) 
		{
			dodge();
		}
		else if(input_interact)
		{
			//if there is something to interact with then interact
			Debug.Log("interacting");
		}
//		else if (input_rla | input_rha | input_lla | input_lha)
//		{
//			//FOR LATER: if an attack animation is currently being played and the animation is not the last in the combo string; queue up comboing attack animation
//			//Play corresponding attack animation
//			//Allow it to apply root motion
//			Debug.Log("attacking");
//		}
		//if motion input is detected move character 
		else if(input_h > 0.0f | input_v > 0.0f)
		{
			//Debug.Log ("Applying Motion");
			//set sprinting variable in animator
			anim.SetBool("isSprinting", input_sprint);

			motionValue = Mathf.Abs(input_h) + Mathf.Abs(input_v);
			
			anim.SetFloat("motionValue", motionValue);
			//move the character 
			// calculate camera relative direction to move:
			camera_ForwardVector = Vector3.Scale(main_Camera.forward, new Vector3(1, 0, 1)).normalized;
			move_Vector = input_v*camera_ForwardVector + input_h*main_Camera.right;

			// pass all parameters to the character control script
			Move(move_Vector);

		}

//		if (!input_sprint)
//			anim.SetBool ("isSprinting", input_sprint);
//		if (!input_dodge)
//			anim.SetBool ("roll", input_dodge);
	}

	public void Move(Vector3 movement_Vector)
	{
		// convert the world relative moveInput vector into a local-relative
		// turn amount and forward amount required to head in the desired
		// direction.
		if (movement_Vector.magnitude > 1f) movement_Vector.Normalize();
		movement_Vector = transform.InverseTransformDirection(movement_Vector);

		m_TurnAmount = Mathf.Atan2(movement_Vector.x, movement_Vector.z);
		m_ForwardAmount = movement_Vector.z;

		float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
		transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);

		// send input and other state parameters to the animator
		//UpdateAnimator(move);
	}

	private void dodge()
	{
		//play rolling animation
		//allow animation to alter root motion
		//if we are rolling turn off colliders for the duration of the roll animation
		//we can play with invincibility frames here

		//if no direction 
		if(input_v == 0.0f && input_h == 0.0f)
		{
			//play jump back animation 
			//disable character collider
			anim.SetBool("jumpBack", input_dodge);
		
		}
		//otherwise roll
		else
		{
			//play roll animation
			//Disable character collider
			anim.SetBool("roll", input_dodge);
		}
		//Enable Character collider
		
	}

	void bendModelBasedUponCameraHeight(Vector3 cameraHeight)
	{
		//bends character model at the hips
		//allows for striking upwards or downwards
		//only up to a + or - a few degrees
		
	}

}


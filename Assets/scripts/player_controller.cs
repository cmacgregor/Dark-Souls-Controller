using UnityEngine;
using System.Collections;

public class player_controller : MonoBehaviour {

	//character variables 
	public Rigidbody rbody;
	public Animator anim;

	//camera movement
	private Transform main_cam;
	private Vector3 cam_forward;


	//character movement
	private float input_h = 0.0f;
	private float input_v = 0.0f;
	private Vector3 move_vector;
	private float turn_amount;
	private float forward_amount;

	//animaition states
	private bool walking = false;


	// Use this for initialization
	void Start () {
		rbody = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();

		main_cam = Camera.main.transform;
	}

	// Update is called once per frame
	void Update () {
		//reset state information
		walking = false;

		input_h = Input.GetAxis ("Horizontal");
		input_v = Input.GetAxis ("Vertical");

		//get cameras pointing direction 
		cam_forward = Vector3.Scale (main_cam.forward, new Vector3 (1, 0, 1)).normalized;
		move_vector = input_v * cam_forward + input_h * main_cam.right;	
		if (input_v != 0) {
			walking = true;
		}

		anim.SetBool ("walking", walking);

		//Move (move_vector);
	}

	void Move(Vector3 move){
		if (move.magnitude > 1f)
			move.Normalize ();
		move = transform.InverseTransformDirection (move);

		turn_amount = Mathf.Atan2 (move.x, move.z);
		forward_amount = move.z;

		rbody.velocity = new Vector3 (move.x, 0, move.y);

	}
		

}

using UnityEngine;
using System.Collections;

public class unimover : MonoBehaviour {

	public new Animator animation;
	public Rigidbody rbody;

	//character movement
	private float input_h;
	private float input_v;
	private bool running;
	private bool sliding;
	private bool jumping; 

	// Use this for initialization
	void Start () {
		animation = GetComponent<Animator> ();
		rbody = GetComponent<Rigidbody> ();
		running = false;
		sliding = false;
		jumping = false;
	}
	
	// Update is called once per frame
	void Update () {
		//get the forward direction of the camera
		//rbody.MoveRotation = Camera.main.transform.TransformDirection(Vector3.forward);

		input_h = Input.GetAxis("Horizontal");
		input_v = Input.GetAxis ("Vertical");

		animation.SetFloat("input_h", input_h);
		animation.SetFloat("input_v", input_v);
		animation.SetBool ("running", running);
		animation.SetBool ("jump", jumping);
		animation.SetBool ("slide", sliding);

		float moveX = input_h * 30f * Time.deltaTime;
		float moveZ = input_v * 50f * Time.deltaTime;

		if (input_v < 0) {
			
		}
		//face right
		if (input_h < 0) {

		}
		//face left
		else {

		}

		//move ya body
		rbody.velocity = new Vector3 (moveX, 0f, moveZ);

		//prevent turning without forward motion
		//if (moveZ <= 0f) {
		//	moveX = 0f;
		//} 
		if (running) {
			moveX *= 3f;
			moveZ *= 3f;
		}
		//running
		if (Input.GetAxis("Run") > 0f && moveZ > 0f) {
			running = true;
		} else 
			running = false;
		//jumping
		if (Input.GetAxis("Jump") > 0f && input_v >= 0f) {
			jumping = true;
		} else { 
			jumping = false;
		}
		//sliding
		if (moveZ > 0f && Input.GetAxis("Sliding") > 0f) {
			sliding = true;
		} else {
			sliding = false;
		}
		//send running

	}
}

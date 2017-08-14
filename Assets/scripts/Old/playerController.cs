using UnityEngine;
using System.Collections;
//using CameraUtilities; 

public class playerController : MonoBehaviour {
	
	private Transform main_cam;						//reference to main camera
	private	Vector3 main_camForward;				//main camera's forward direction

	private CharacterController p_Character;	//referene to player character class
	private Vector3 p_Move; 						//product direction of main cameras direction and user input 

	private void Start()
	{
		//get main camera
		main_cam = Camera.main.transform;


		//set it's transforms to the approrpitae defualts 
		InitializeCameratoDefualts(main_cam);

		p_Character = GetComponent<CharacterController>();
	}

	private void FixedUpdate()
	{
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		//calculate movement based on camera
		main_camForward = Vector3.Scale(main_cam.forward, new Vector3(1, 0, 1)).normalized;
		p_Move = v * main_camForward + h * main_cam.right;
	
		p_Character.Move (p_Move);
	}



	void InitializeCameratoDefualts(Transform cam_pos)
	{
		//stub
		Debug.Log ("Initializing Camera");
	}
}

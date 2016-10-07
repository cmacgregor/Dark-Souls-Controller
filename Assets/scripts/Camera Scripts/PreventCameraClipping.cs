using UnityEngine;
using System.Collections;

public class PreventCameraClipping : MonoBehaviour {

	public const int CAMERA_MAX_DISTANCE = 7;
	public GameObject cam_reference;
	public Camera main_cam; 

	void start()
	{
		Debug.Log ("Initializing");
		//if we aren't given a reference then default to a thing
		if (cam_reference == null)
		{
			cam_reference = GetComponent<GameObject> ();
		}
		//If we aren't given a camera then default to the main camera
		if (main_cam == null)
		{
			main_cam = Camera.main;
		}
	}

	void update() 
	{
		Vector3 rayOrigin = cam_reference.transform.position; //camera reference object position
		RaycastHit hit; 

		if(Physics.Raycast(rayOrigin, main_cam.transform.position, out hit, CAMERA_MAX_DISTANCE))
		{			
			Debug.Log ("Hit detected, need to move camera");
			//move main camera forward to not clip
			main_cam.transform.position = hit.transform.position;
		}
	}
}

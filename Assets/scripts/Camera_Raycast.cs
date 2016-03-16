using UnityEngine;
using System.Collections;

public class Camera_Raycast : MonoBehaviour {

	public static float raycast_distance = camerafollow.distance;
	private RaycastHit hit;

	// Update is called once per frame
	void Update () {
		//lookat camera
		transform.LookAt (Camera.main.transform);

		//raycast
		if(Physics.Raycast(transform.position, Vector3.forward, camerafollow.distance)) {
			raycast_distance = hit.distance;
		}
	}
}

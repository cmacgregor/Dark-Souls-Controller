using UnityEngine;
using System.Collections;

public class camerafollow : MonoBehaviour {

	public Transform target;

	private const float Y_ANGLE_MIN = -50.0f;
	private const float Y_ANGLE_MAX = 50.0f;

	public static float distance = 5.0f;
	public float x_sensitivity = 2.0f;
	public float y_sensitivity = 2.0f;

	private float x_current = 0.0f;
	private float y_current = 0.0f;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		//could create syncing issues between player and camera
		offset = (transform.position - target.position).normalized * distance;
		//transform.position = target.position + offset;
	}

	// Update is called once per frame
	void Update () {
		x_current -= Input.GetAxis ("Mouse X") * x_sensitivity;
		y_current -= Input.GetAxis ("Mouse Y") * y_sensitivity;

		y_current = Mathf.Clamp (y_current, Y_ANGLE_MIN, Y_ANGLE_MAX);

		//Debug.Log(offset);
		//Vector3 direction = new Vector3 (0, 0, -distance);
		Quaternion rotation = Quaternion.Euler (y_current, x_current, 0);
		//use raycaster to deteremine if we're stuck or so that we can't go through terrain
		if (Camera_Raycast.raycast_distance < distance) {
			Debug.Log (Camera_Raycast.raycast_distance);
		}// else {
		transform.position = target.position + rotation * offset;
		//}
		//Debug.Log (Camera_Raycast.raycast_distance);
		transform.LookAt (target.position);

	}
}


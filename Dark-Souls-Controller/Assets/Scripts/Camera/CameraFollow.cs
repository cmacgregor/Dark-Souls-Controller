using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float distance = 5.0f;

	public float x_sensitivity = 2.0f;
	public float y_sensitivity = 2.0f;

	private float x_current = 0.0f;
	private float y_current = 0.0f;

	private const float Y_ANGLE_MIN = -50.0f;
	private const float Y_ANGLE_MAX = 50.0f;

	private Vector3 offset;

	//initialization
	void Start () {

		//could create syncing issues between player and camera
		offset = (transform.position - target.position).normalized * distance;

	}

	// Update is called once per frame
	void Update () {
		x_current += Input.GetAxis ("CameraX") * x_sensitivity;
		y_current += Input.GetAxis ("CameraY") * y_sensitivity;

		y_current = Mathf.Clamp (y_current, Y_ANGLE_MIN, Y_ANGLE_MAX);
	}

	private void LateUpdate(){
		Quaternion rotation = Quaternion.Euler (y_current, x_current, 0);
		transform.position = target.position + rotation * offset;
		transform.LookAt (target.position);
	}
}

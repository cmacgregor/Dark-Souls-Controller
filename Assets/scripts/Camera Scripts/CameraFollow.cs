using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Camera main_Camera;
	public Transform target;

	public float x_sensitivity = 2.0f;
	public float y_sensitivity = 2.0f;

	private float x_current = 0.0f;
	private float y_current = 0.0f;

	private const float Y_ANGLE_MIN = -50.0f;
	private const float Y_ANGLE_MAX = 50.0f;

    private float Distance;
    private float veLDistance = 0f;
    public float desiredDistance = 5.0f;

    private Vector3 desiredPosition;
	private Vector3 offset;

	//initialization
	void Start () {
        if (Camera.main == null){
            //Generate main camera

        }
        main_Camera = Camera.main;
		//could create syncing issues between player and camera
		offset = (transform.position - target.position).normalized * desiredDistance;
	}

	// Update is called once per frame
	void Update () {
		x_current += Input.GetAxis ("Mouse X") * x_sensitivity;
		y_current += Input.GetAxis ("Mouse Y") * y_sensitivity;
		y_current = Mathf.Clamp (y_current, Y_ANGLE_MIN, Y_ANGLE_MAX);
	}

	private void LateUpdate(){
		Quaternion rotation = Quaternion.Euler (y_current, x_current, 0);
		transform.position = target.position + rotation * offset;
		transform.LookAt (target.position);

        CalculateDesiredPosition();
        CheckCameraPoints(target.position, desiredPosition);
    }

    float CheckCameraPoints(Vector3 from, Vector3 to)
    {
        var nearDistance = -1f;
        RaycastHit hitInfo;
        Helper.clipPlanePoints clipPlanePoints = Helper.ClipPlanePoints(to);
        //draw lines in the editor to visualize raycasts 
        Debug.DrawLine(from, to + transform.forward * -main_Camera.nearClipPlane, Color.red);
        Debug.DrawLine(from, clipPlanePoints.UpperLeft);
        Debug.DrawLine(from, clipPlanePoints.UpperRight);
        Debug.DrawLine(from, clipPlanePoints.LowerLeft);
        Debug.DrawLine(from, clipPlanePoints.LowerRight);
        //Form box to simulate plane at the end of raycasts 
        Debug.DrawLine(clipPlanePoints.UpperLeft, clipPlanePoints.UpperRight);
        Debug.DrawLine(clipPlanePoints.UpperLeft, clipPlanePoints.LowerLeft);
        Debug.DrawLine(clipPlanePoints.UpperRight, clipPlanePoints.LowerRight);
        Debug.DrawLine(clipPlanePoints.LowerLeft, clipPlanePoints.LowerRight);

        if (Physics.Linecast(from, clipPlanePoints.UpperLeft, out hitInfo) && hitInfo.collider.tag != "Player")
			nearDistance = hitInfo.distance;
        if (Physics.Linecast(from, clipPlanePoints.LowerLeft, out hitInfo) && hitInfo.collider.tag != "Player")
			if (hitInfo.distance < nearDistance || nearDistance == -1)
            nearDistance = hitInfo.distance;
        if (Physics.Linecast(from, clipPlanePoints.UpperRight, out hitInfo) && hitInfo.collider.tag != "Player")
			if (hitInfo.distance < nearDistance || nearDistance == -1)
            nearDistance = hitInfo.distance;
        if (Physics.Linecast(from, clipPlanePoints.LowerRight, out hitInfo) && hitInfo.collider.tag != "Player")
			if (hitInfo.distance < nearDistance || nearDistance == -1)
            nearDistance = hitInfo.distance;
        if (Physics.Linecast(from, to + transform.forward * -main_Camera.nearClipPlane, out hitInfo) && hitInfo.collider.tag != "Player")
			if (hitInfo.distance < nearDistance || nearDistance == -1)
            nearDistance = hitInfo.distance;

        return nearDistance;
    }

    void CalculateDesiredPosition()
    {
        Distance = Mathf.SmoothDamp(Distance, desiredDistance, ref velDistance, DistanceSmooth);
        desiredPosition = CalculatePosition(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), Distance);
    }

    Vector3 CalculatePosition(float rotationX, float rotationY, float distance)
    {
        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        return target.position + rotation * direction;
    }

    static class Helper
    {
        public struct clipPlanePoints
        {
            public Vector3 UpperLeft;
            public Vector3 LowerLeft;
            public Vector3 UpperRight;
            public Vector3 LowerRight;
        }

        public static float ClampAngle(float angle, float min, float max)
        {
            do
            {
                if (angle < -360) angle += 360;
                if (angle > 360) angle -= 360;
            } while (angle < -360 || angle > 360);

            return Mathf.Clamp(angle, min, max);
        }

        public static clipPlanePoints ClipPlanePoints(Vector3 pos)
        {
            var clipPlanePoints = new clipPlanePoints();
            //ensure main camera exists
            if (Camera.main == null)
                return clipPlanePoints;

            var cameraTransform = Camera.main.transform;
            var halfFOV = (Camera.main.fieldOfView / 2) * Mathf.Deg2Rad;
            var aspect = Camera.main.aspect;
            var distance = Camera.main.nearClipPlane;
            var height = distance * Mathf.Tan(halfFOV);
            var width = height * aspect;

            clipPlanePoints.LowerRight = pos + cameraTransform.right * width;
            clipPlanePoints.LowerRight -= cameraTransform.up * height;
            clipPlanePoints.LowerRight += cameraTransform.forward * distance;

            clipPlanePoints.LowerLeft = pos - cameraTransform.right * width;
            clipPlanePoints.LowerLeft -= cameraTransform.up * height;
            clipPlanePoints.LowerLeft += cameraTransform.forward * distance;

            clipPlanePoints.UpperRight = pos + cameraTransform.right * width;
            clipPlanePoints.UpperRight += cameraTransform.up * height;
            clipPlanePoints.UpperRight += cameraTransform.forward * distance;

            clipPlanePoints.UpperLeft = pos - cameraTransform.right * width;
            clipPlanePoints.UpperLeft += cameraTransform.up * height;
            clipPlanePoints.UpperLeft += cameraTransform.forward * distance;

            return clipPlanePoints;
        }
    }
}

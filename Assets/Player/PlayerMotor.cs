using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

	[SerializeField]
	private Camera cam;
    public Camera GetCamera()
    {
        return cam;
    }
    
	private Vector3 rotation = Vector3.zero;
	private float cameraRotationX = 0f;
	private float currentCameraRotationX = 0f;

	[SerializeField] private float cameraRotationLimit = 85f;

	private Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}

	// Gets a rotational vector
	public void Rotate(Vector3 _rotation)
	{
		rotation = _rotation;
	}

	// Gets a rotational vector for the camera
	public void RotateCamera(float _cameraRotationX)
	{
		cameraRotationX = _cameraRotationX;
	}

	// Run every physics iteration
	void FixedUpdate ()
	{
		PerformRotation();
	}

	//Perform rotation
	void PerformRotation ()
	{
		rb.MoveRotation(rb.rotation * Quaternion.Euler (rotation));
		if (cam != null)
		{
			// Set our rotation and clamp it
			currentCameraRotationX -= cameraRotationX;
			currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

			//Apply our rotation to the transform of our camera
			cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
		}
	}

    public void ApplyKnockback(int force)
    {
        rb.AddForce(-cam.transform.forward * force);
    }
}

using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

	[SerializeField]
	private float lookSensitivity = 3f;

	[SerializeField]
	private LayerMask environmentMask;

	private PlayerMotor motor;

	void Start ()
	{
		motor = GetComponent<PlayerMotor>();
         
        Cursor.lockState = CursorLockMode.Locked; // TODO Remove and make more useful
	}

	void Update ()
	{
        if (PauseMenu.IsOn)
        {
            return;
        }

        //Calculate rotation as a 3D vector (turning around)
        float _yRot = Input.GetAxisRaw("Mouse X");

		Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

		//Apply rotation
		motor.Rotate(_rotation);

		//Calculate camera rotation as a 3D vector (turning around)
		float _xRot = Input.GetAxisRaw("Mouse Y");

		float _cameraRotationX = _xRot * lookSensitivity;

		//Apply camera rotation
		motor.RotateCamera(_cameraRotationX);
	}
}

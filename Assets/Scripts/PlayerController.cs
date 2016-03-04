using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	[SerializeField]
	private float speed = .5f;

	private Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
			return;
		Vector3 movementVector = Vector3.zero;

		Vector3 forward = Camera.main.transform.rotation * Vector3.forward;
		forward = new Vector3 (forward.x, 0, forward.z);
		Vector3 left = Quaternion.Euler (0, -90, 0) * forward;
		Vector3 right = Quaternion.Euler (0, 90, 0) * forward;
		Vector3 back = Quaternion.Euler (0, 180, 0) * forward;

		if (Input.GetKey (KeyCode.W))
			movementVector += forward;
		if (Input.GetKey (KeyCode.A))
			movementVector += left;
		if (Input.GetKey (KeyCode.S))
			movementVector += back;
		if (Input.GetKey (KeyCode.D))
			movementVector += right;
		
		movementVector.Normalize ();
		movementVector *= speed;

		transform.position += movementVector;
	}
}

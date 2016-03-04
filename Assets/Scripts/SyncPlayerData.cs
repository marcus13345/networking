using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
/*
public class SyncPlayerData : NetworkBehaviour {

	struct Data {
		public Vector3 position;
	}

	[SyncVar]
	private Data data;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		updateData ();
	}

	[ClientCallback]
	private void updateData() {
		Data send = new Data {
			position = transform.position
		};
		CmdSendData (send);
		if (!isLocalPlayer) {
			transform.position = Vector3.Lerp (transform.position, data.position, Time.deltaTime * 8);
		}
	}

	[Command]
	private void CmdSendData(Data send) {
		data = send;
	}
}
*/
public class SyncPlayerData : NetworkBehaviour {
	/*
	[SyncVar]
	private Vector3 syncPosition;

	public void update() {
		DebugConsole.Log ("poopfart", "normal");
		Debug.Log ("");
		if (isLocalPlayer && !isServer) {
			CmdSendPosition (transform.position);
		}
		if (!isLocalPlayer) {
			transform.position = syncPosition;
		}
	}

	[Command]
	public void CmdSendPosition(Vector3 newPosition) {
		syncPosition = newPosition;
	}*/

	[SerializeField]
	private TextMesh label;

	[SerializeField]
	private Rigidbody rigidbody;

	[SyncVar]
	public string playerName = "Player";
	[SyncVar]
	public float x = 0;
	[SyncVar]
	public float y = 0;
	[SyncVar]
	public float z = 0;

	public void OnGUI() {
		label.text = playerName;
		if (isLocalPlayer) {
			playerName = GUI.TextField (new Rect (25, Screen.height - 40, 100, 30), playerName);
			serializeData ();
			CmdSendData (x, y, z, playerName);
		} else {
			//if we're not controlling this, then our syncvar got updated.
			//so we need to deserialze that.
			deserializeData();
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
		}
	}

	/// <summary>
	/// pull relevant information from gameobject into local variables
	/// </summary>
	private void serializeData() {
		x = transform.position.x;
		y = transform.position.y;
		z = transform.position.z;
	}

	/// <summary>
	/// same idea as deserialize data, except instead of being instantaneous and only called once, this is called
	/// once every frame with update and is used to reduce latency stutter.
	/// </summary>
	private void lerpToData() {
		
	}

	/// <summary>
	/// push local varialbes back in to the game object.
	/// </summary>
	private void deserializeData() {
		
		//transform.position = new Vector3(x, y, z);
	}

	/// <summary>
	/// pushes variables to the servers locals, then deserializes them on the server side.
	/// </summary>
	/// <param name="newX">New x.</param>
	/// <param name="newY">New y.</param>
	/// <param name="newZ">New z.</param>
	/// <param name="newPlayerName">New player name.</param>
	[Command]
	private void CmdSendData(float newX, float newY, float newZ, string newPlayerName) {
		x = newX;
		y = newY;
		z = newZ;
		playerName = newPlayerName;
		deserializeData ();
	}
}
/*
if local and !server
command out

if not local
read in
*/


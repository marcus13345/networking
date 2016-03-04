using UnityEngine;
using System.Collections;

public class Proximity : MonoBehaviour {

	private Vector3 startingPosition;
	private Renderer renderer;

	[SerializeField]
	private Vector3 animation = new Vector3(0, 10, 0);

	[SerializeField]
	private float animationSpeed = 8f;

	[SerializeField]
	private float triggerDistance = 5f;

	private float animationTime = 0;

	// Use this for initialization
	void Start () {
		startingPosition = gameObject.transform.position;
		renderer = GetComponent<Renderer> ();
		goToPosition (false);
	}
	
	// Update is called once per frame
	void Update () {
		goToPosition (true);
	}

	private void goToPosition(bool lerp) {

		float distance = float.MaxValue;
		foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) {
			distance = Mathf.Min (distance, Vector3.Distance (player.transform.position, startingPosition));
		}
		bool trigger = distance < triggerDistance & distance != float.MaxValue;

		Vector3 target = trigger ? startingPosition : startingPosition - animation;
		gameObject.transform.position = lerp ? Vector3.Lerp (gameObject.transform.position, target, (1f/animationSpeed) * Time.deltaTime/.017f) : target;

		animationTime = Mathf.Lerp (animationTime, trigger ? 1 : 0, (1f / animationSpeed) * Time.deltaTime/.017f);
		if (Mathf.Abs (animationTime - 1) < .5f) {
			renderer.enabled = true;
		} else {
			renderer.enabled = false;
		}
	}

}

using UnityEngine;
using System.Collections;

public class FiredArrowController : MonoBehaviour {
	private SceneController sceneController;
	private int kills = 0;
	public float speed;
	
	void Start () {
		sceneController = Camera.main.GetComponent<SceneController>();

		#if UNITY_IPHONE
			speed = 0.6f;
		#else
			speed = 0.4f;
		#endif
	}

	void Update () {
		if (transform.gameObject.tag == "FiredArrow") {
			transform.position = new Vector2 (transform.position.x + speed, transform.position.y);
		}

		if (transform.position.x >= 6.62f) {
			// Arrow has left the scene so get rid of it

			if (kills > 1) {
				sceneController.textMessageQueue.Enqueue (kills.ToString() + " flies with one arrow!");
			}

			Destroy(transform.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Fly") == true) {
			kills++;
		}
	}
}

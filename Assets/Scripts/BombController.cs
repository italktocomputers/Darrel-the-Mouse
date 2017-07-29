using UnityEngine;
using System.Collections;

public class BombController : MonoBehaviour {
	public float speed;
	private SceneController sceneController;
	
	void Start () {
		sceneController = Camera.main.GetComponent<SceneController>();

		#if UNITY_IPHONE
			speed = 0.07f;
		#else
			speed = 0.03f;
		#endif
	}

	void Update () {
		transform.position = new Vector2(transform.position.x-speed, transform.position.y);

		if (transform.position.x <= -7.0f) {
			// Item has left the scene so remove it
			Destroy(transform.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Mouse") {
			AudioController.playBangSound ();
			Destroy(transform.gameObject);
			sceneController.destroyAllEnemies();
		}
	}
}

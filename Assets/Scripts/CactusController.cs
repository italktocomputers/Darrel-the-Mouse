using UnityEngine;
using System.Collections;

public class CactusController : MonoBehaviour {
	private SceneController sceneController;
	public float speed;

	void Start () {
		sceneController = Camera.main.GetComponent<SceneController>();

		#if UNITY_IPHONE
		speed = 0.2f;
		#else
		speed = 0.009f;
		#endif
	}

	void Update () {
		transform.position = new Vector2 (transform.position.x - speed, transform.position.y);

		if (transform.position.x <= -7.0f) {
			// Object has left the scene so remove it from the scene
			Destroy(transform.gameObject);
		}
	}
}

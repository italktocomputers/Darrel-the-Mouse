using UnityEngine;
using System.Collections;

public class TumbleweedController : MonoBehaviour {
	private SceneController sceneController;
	public int kills = 0;
	public float speed;

	void Start () {
		sceneController = Camera.main.GetComponent<SceneController>();

		#if UNITY_IPHONE
			speed = 0.2f;
		#else
			speed = 0.07f;
		#endif
	}

	void Update () {
		transform.position = new Vector2 (transform.position.x - speed, transform.position.y);
		transform.Rotate (0.0f,0.0f,((Time.deltaTime) * 400.0f));

		if (transform.position.x <= -7.0f) {
			// Object has left the scene so remove it from the scene
			Destroy(transform.gameObject);
		}
	}
}

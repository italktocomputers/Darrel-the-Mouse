using UnityEngine;
using System.Collections;

public class DustStormController : MonoBehaviour {
	private SceneController sceneController;
	public int kills = 0;

	void Start () {
		sceneController = Camera.main.GetComponent<SceneController>();
	}

	void Update () {
		if (transform.gameObject.tag == "DustStorm") {
			transform.position = new Vector2 (transform.position.x - 0.1f, transform.position.y);
		}

		if (transform.position.x <= -7.0f) {
			// Object has left the scene so remove it from the scene
			Destroy(transform.gameObject);
		}
	}
}

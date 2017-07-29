using UnityEngine;
using System.Collections;

public class QuiverController : MonoBehaviour {
	private SceneController sceneController;
	private Animator animator;
	private float speed;

	void Start () {
		sceneController = Camera.main.GetComponent<SceneController>();
		animator = sceneController.Mouse.GetComponent<Animator> ();

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
			AudioController.playPowerSound ();
			Destroy(transform.gameObject);
			
			MouseController controller = other.gameObject.GetComponent<MouseController>();

			if (transform.gameObject.tag == "Quiver") {
				controller.arrows += 6;
				sceneController.UpdateArrowLabel(controller.arrows);
				animator.SetBool ("hasWeapon", true);
			}
		}
	}
}

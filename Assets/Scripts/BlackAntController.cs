using UnityEngine;
using System.Collections;

public class BlackAntController : MonoBehaviour {
	private Animator animator;
	private SceneController sceneController;
	public Vector3 coords;

	void Start () {
		animator = GetComponent<Animator> ();
		sceneController = Camera.main.GetComponent<SceneController>();
		coords = CommonFunctions.getTopLeftCoords ();
	}

	void Update () {
		
		#if UNITY_IPHONE
			float speed = 0.07f;
		#else
			float speed = 0.04f;
		#endif

		if (animator.GetBool ("Dead") == true) {
			// Poof should follow foreground
			transform.position = new Vector2(transform.position.x-0.03f, transform.position.y);
		} else {
			transform.position = new Vector3 (transform.position.x - speed, transform.position.y, 0);
		}
		
		if (transform.position.x <= coords.x + -0.5f) {
			// Ant has left the scene so remove it from the scene
			Destroy(transform.gameObject);
		}

		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Die")) {
			// Remove ant from scene
			Destroy(transform.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Rock") {
			kill ();
		} else {
			for (int i=0; i<21; i++) {
				if (other.tag == "RedRock" + (i + 1).ToString ()) {
					kill ();
				}

				if (other.tag == "BlueRock" + (i + 1).ToString ()) {
					kill ();
				}
			}
		}
	}

	public void kill() {
		if (animator.GetBool ("Dead") == false) {
			// Update score
			ApplicationModel.score++;
			ApplicationModel.clearStreak++;

			sceneController.UpdateScoreboardLabel ();
			sceneController.UpdateClearStreakLabel ();

			animator.SetBool ("Dead", true);
			AudioController.playPoofSound ();
		}
	}
}

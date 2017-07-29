using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class FlyController : MonoBehaviour {
	private Animator animator;
	private SceneController sceneController;
	private BackgroundController backgroundController;
	public float speed;
	public List<Vector2> paths;
	public int indexPath;
	public Vector3 coords;

	void Start () {
		animator = GetComponent<Animator> ();
		sceneController = Camera.main.GetComponent<SceneController>();
		backgroundController = Camera.main.GetComponent<BackgroundController>();
		indexPath = 0;
		paths = new List<Vector2>();
		coords = CommonFunctions.getTopLeftCoords ();

		#if UNITY_IPHONE
			speed = 5.0f;
		#else
			speed = 5.0f;
		#endif

		for (int i=0; i<4; i++) {
			float x = UnityEngine.Random.Range (coords.x, -coords.x);
			float y = UnityEngine.Random.Range (coords.y, -coords.y+1.5f);
			paths.Add(new Vector2 (x,y));
		}

		// Exit path
		paths.Add (new Vector2(-11.0f, transform.position.y));
	}

	void Update () {
		if (animator.GetBool ("Dead") == true) {
			// Poof should follow foreground
			transform.position = new Vector2(transform.position.x-backgroundController.foregroundSpeed, transform.position.y);
		} else {
			float step = speed * Time.deltaTime;
			transform.position = Vector2.MoveTowards (transform.position, paths [indexPath], step);
		}
		
		if (transform.position.x <= coords.x + -0.5f) {
			// Fly has left the scene so remove it from the scene
			Destroy(transform.gameObject);
		}
		
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Die")) {
			// Remove fly from scene
			Destroy(transform.gameObject);
		}

		if (transform.position.x == paths [indexPath].x && transform.position.y == paths [indexPath].y) {
			// Fly has reached the end of their path so let's start them on a new path
			indexPath++;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "FiredArrow" && animator.GetBool ("Dead") != true) {
			kill ();
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

using UnityEngine;
using System.Collections;
using System;

public class MouseController : MonoBehaviour {
	private Rigidbody2D rb;
	private Animator animator;
	public bool canJump;
	public GameObject rock;
	public int lives;
	SceneController sceneController;
	public DateTime bangImgShown;
	private bool isDead = false;
	private DateTime gameOverDelay;
	private bool calledGameOverFunc = false;
	public int arrows;
	public DateTime didJump;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		canJump = true;
		sceneController = Camera.main.GetComponent<SceneController>();
		bangImgShown = DateTime.Today;
		lives = 3;
		arrows = 10;

		sceneController.UpdateLifeLabel (lives);
		sceneController.UpdateArrowLabel (arrows);
	}
	
	void Update () {
		DateTime now = DateTime.Now;

		TimeSpan timespanJump = (DateTime.Now - didJump).Duration ();

		if (timespanJump.TotalSeconds > 0.7 && animator.GetBool("shouldJump") == true) {
			animator.SetBool ("shouldWalk", false);
			animator.SetBool ("shouldJump", false);
			animator.SetBool ("shouldFall", true);
		}

		if (isDead == true) {
			TimeSpan timespanDie = (DateTime.Now - gameOverDelay).Duration ();

			if (timespanDie.TotalSeconds < 2.0) {
				// Spin Mouse for 2 seconds
				transform.Rotate (Vector3.forward, 6.0f * 100.0f * Time.deltaTime, 0);
				transform.localScale += new Vector3 (0.1f, 0.1f, 0);
			} else {
				// Remove from the scene
				this.transform.position = new Vector2(-100, -100);
			}

			if (timespanDie.TotalSeconds >= 2.5 && calledGameOverFunc == false) {
				calledGameOverFunc = true;
				sceneController.gameOver ();
			}
		} else {
			if (Input.GetMouseButtonDown (0) == true) {
				if (canJump == true && transform.position.y <= -1.54f) {
					// Apply jump
					didJump = DateTime.Now;

					rb.AddForce (transform.up * 470.0f);
					animator.SetBool ("shouldWalk", false);
					animator.SetBool ("shouldJump", true);
					animator.SetBool ("shouldFall", false);
					canJump = false;

					AudioController.playJumpSound ();
				} else {
					// Throw rock
					fireArrow ();
				}
			}
		}

		TimeSpan timespan = (now - bangImgShown).Duration();
		
		if (timespan.TotalSeconds >= 0.1) {
			GameObject[] gameObjects =  GameObject.FindGameObjectsWithTag ("Bang");
			
			for(int i = 0 ; i < gameObjects.Length; i++) {
				Destroy(gameObjects[i]);
			}
		}
	}

	private void die() {
		AudioController.stopMusic ();

		BackgroundController bgCtrl = sceneController.GetComponent<BackgroundController>();
		bgCtrl.stop = true;

		sceneController.isGameOver = true;
		sceneController.removeAllItems ();

		AudioController.playGameOverSound ();

		gameOverDelay = DateTime.Now;

		GetComponent<Rigidbody2D>().isKinematic = true;
		GetComponent<BoxCollider2D> ().enabled = false;

		sceneController.panel.SetActive (true);
	}

	private void fireArrow() {
		if (animator.GetBool ("hasWeapon") == true) {
			arrows--;

			sceneController.UpdateArrowLabel (arrows);

			Sprite backSprite = Resources.Load ("arrow", typeof(Sprite)) as Sprite;
			GameObject obj = new GameObject ("FiredArrow");
			obj.tag = "FiredArrow";

			obj.transform.position = new Vector2 (transform.position.x+0.5f, transform.position.y);

			obj.AddComponent<FiredArrowController> ();
		
			SpriteRenderer rend = obj.AddComponent (typeof(SpriteRenderer)) as SpriteRenderer;
			rend.sprite = backSprite;
			rend.sortingOrder = 2;

			BoxCollider2D bc = obj.AddComponent (typeof(BoxCollider2D)) as BoxCollider2D;
			bc.isTrigger = true;

			if (arrows <= 0) {
				animator.SetBool ("hasWeapon", false);
			}
		}
	}

	private void takeHit() {
		lives--;
		sceneController.UpdateLifeLabel (lives);
		AudioController.playHitSound ();

		ApplicationModel.clearStreak = 0;
		sceneController.UpdateClearStreakLabel ();

		if (isDead != true && lives == 0) {
			isDead = true;
			die ();
		}
		
		//Sprite backSprite = Resources.Load("bang", typeof(Sprite)) as Sprite;
		//GameObject obj = new GameObject("bang");
		//obj.tag = "Bang";
		//obj.transform.position = new Vector2 (transform.position.x, transform.position.y);
		
		//SpriteRenderer renderer = obj.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;
		//renderer.sprite = backSprite;
		//renderer.sortingOrder = 2;
		//bangImgShown = DateTime.Now;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "BlackAnt") {
			BlackAntController controller = other.gameObject.GetComponent<BlackAntController> ();
			Animator animator = other.gameObject.GetComponent<Animator> ();

			if (animator.GetBool ("Dead") != true) {
				if (rb.velocity.y < 0) {
					// Bounce if jumping on ant
					rb.velocity = Vector3.zero;
					rb.AddForce (transform.up * 400.0f);
					// We will kill ant from here and not from in the AntController
					// because of a race condition
					controller.kill ();
				} else {
					takeHit ();
					// We will kill ant from here and not from in the AntController
					// because of a race condition
					controller.kill ();
				}
			}
		} else if (other.gameObject.tag == "Fly") {
			FlyController controller = other.gameObject.GetComponent<FlyController> ();
			Animator animator = other.gameObject.GetComponent<Animator> ();
			
			if (animator.GetBool ("Dead") != true) {
				takeHit ();
				controller.kill ();
			}
		} else if (other.gameObject.tag == "Tumbleweed") {
			takeHit ();
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.CompareTag ("Foreground1") == true || other.gameObject.CompareTag ("Foreground2") == true) {
			// Mouse is on the ground
			animator.SetBool ("shouldJump", false);
			animator.SetBool ("shouldFall", false);
			animator.SetBool ("shouldWalk", true);
			canJump = true;
		}
	}
}

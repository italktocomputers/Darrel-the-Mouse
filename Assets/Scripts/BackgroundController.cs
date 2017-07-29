using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {
	public GameObject background1;
	public GameObject background2;

	public GameObject hills1;
	public GameObject hills2;

	public GameObject foreground1;
	public GameObject foreground2;

	private float background1Position;
	private float background1Start;
	private float background2Position;
	private float background2Start;

	private float hills1Position;
	private float hills1Start;
	private float hills2Position;
	private float hills2Start;

	private float foreground1Position;
	private float foreground1Start;
	private float foreground2Position;
	private float foreground2Start;

	public float backgroundSpeed;
	public float hillsSpeed;
	public float foregroundSpeed;

	private float length;

	public bool stop = false;
	
	void Start () {
		background1 = GameObject.FindWithTag("Background1");
		background2 = GameObject.FindWithTag("Background2");

		hills1 = GameObject.FindWithTag("Hills1");
		hills2 = GameObject.FindWithTag("Hills2");

		foreground1 = GameObject.FindWithTag("Foreground1");
		foreground2 = GameObject.FindWithTag("Foreground2");

		background1Start = 0;
		background2Start = 19.98f;

		hills1Start = 0;
		hills2Start = 19.98f;

		foreground1Start = 0;
		foreground2Start = 20.00f;

		background1Position = background1Start;
		background2Position = background2Start;

		hills1Position = hills1Start;
		hills2Position = hills2Start;

		foreground1Position = foreground1Start;
		foreground2Position = foreground2Start;

		backgroundSpeed = 0.001f;
		hillsSpeed = 0.003f;
		//foregroundSpeed = 0.03f;
		foregroundSpeed = 0.03f;

		#if UNITY_IPHONE
			backgroundSpeed = 0.004f;
			hillsSpeed = 0.04f;
			foregroundSpeed = 0.06f;
		#endif

		length = 20.00f;
	}
	
	void Update () {
		if (!stop) {
			background1.gameObject.transform.position = new Vector2 (background1Position, background1.gameObject.transform.position.y);
			background1Position = background1Position - backgroundSpeed;

			background2.gameObject.transform.position = new Vector2 (background2Position, background2.gameObject.transform.position.y);
			background2Position = background2Position - backgroundSpeed;

			hills1.gameObject.transform.position = new Vector2 (hills1Position, hills1.gameObject.transform.position.y);
			hills1Position = hills1Position - hillsSpeed;

			hills2.gameObject.transform.position = new Vector2 (hills2Position, hills2.gameObject.transform.position.y);
			hills2Position = hills2Position - hillsSpeed;

			foreground1.gameObject.transform.position = new Vector2 (foreground1Position, foreground1.gameObject.transform.position.y);
			foreground1Position = foreground1Position - foregroundSpeed;

			foreground2.gameObject.transform.position = new Vector2 (foreground2Position, foreground2.gameObject.transform.position.y);
			foreground2Position = foreground2Position - foregroundSpeed;

			// Once the foreground is out of the camera view, we want
			// to move it directly behind the preceeding forground.
			if (background1Position < -17.00f) {
				background1Position = background2Position + 20.00f;
			}

			if (background2Position < -17.00f) {
				background2Position = background1Position + 20.00f;
			}

			if (hills1Position < -17.00f) {
				hills1Position = hills2Position + 20.00f;
			}

			if (hills2Position < -17.00f) {
				hills2Position = hills1Position + 20.00f;
			}

			if (foreground1Position < -17.00f) {
				foreground1Position = foreground2Position + 20.00f;
			}

			if (foreground2Position < -17.00f) {
				foreground2Position = foreground1Position + 20.00f;
			}
		}
	}

}

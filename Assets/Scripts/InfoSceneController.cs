using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InfoSceneController : MonoBehaviour, ThemeableScene {
	public GameObject fly;
	public GameObject copyright;

	public GameObject background1;
	public GameObject hills1;
	public GameObject foreground1;

	public GameObject btnBack;

	void Start () {
		CommonFunctions.loadRandomTheme (this, "desert");
		CommonFunctions.makeCopyrightFooter (copyright);

		#if UNITY_EDITOR || UNITY_WEBGL
		fly.SetActive(true);
		#endif
	}

	void Update () {
		#if UNITY_EDITOR || UNITY_WEBGL
			// Control the fly with the mouse.
			Vector3 pointer = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			fly.transform.position = pointer;
		#endif
	}

	public void onStartGameButtonClick() {
		SceneManager.LoadScene ("StartScene");
	}

	public GameObject getBackground1() {
		return background1;
	}

	public GameObject getBackground2() {
		return null;
	}

	public GameObject getHills1() {
		return hills1;
	}

	public GameObject getHills2() {
		return null;
	}

	public GameObject getForeground1() {
		return foreground1;
	}

	public GameObject getForeground2() {
		return null;
	}

	public GameObject getBtnForward() {
		return null;
	}

	public GameObject getBtnBack() {
		return btnBack;
	}

	public GameObject getBtnSettings() {
		return null;
	}

	public GameObject getBtnInfo() {
		return null;
	}

	public GameObject getBtnChecked() {
		return null;
	}

	public GameObject getBtnUnChecked() {
		return null;
	}

	public GameObject getJumbotron() {
		return null;
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsSceneController : MonoBehaviour, ThemeableScene {
	public GameObject fly;
	public GameObject playMusicCheckbox;
	public GameObject saveScoreToLBCheckbox;
	public GameObject clearStatsBtn;
	public GameObject clearStatsConfirmBtn;
	public GameObject clearStatsCancelBtn;
	public GameObject copyright;

	public GameObject qualityFastest;
	public GameObject qualityFast;
	public GameObject qualitySimple;
	public GameObject qualityGood;
	public GameObject qualityBeautiful;
	public GameObject qualityFantastic;

	public GameObject background1;
	public GameObject hills1;
	public GameObject foreground1;

	public GameObject btnBack;
	public GameObject btnChecked;
	public GameObject btnUnChecked;

	void Start () {
		CommonFunctions.loadRandomTheme (this, "desert");
		CommonFunctions.makeCopyrightFooter (copyright);

		#if UNITY_EDITOR || UNITY_WEBGL
		fly.SetActive(true);
		#endif

		if (ApplicationModel.getPlayMusicSetting () == 1) {
			playMusicCheckbox.GetComponent<Image> ().sprite = btnChecked.GetComponent<Image> ().sprite;
		} else {
			playMusicCheckbox.GetComponent<Image> ().sprite = btnUnChecked.GetComponent<Image> ().sprite;
		}

		if (ApplicationModel.getSaveScoreToLBSetting () == 1) {
			saveScoreToLBCheckbox.GetComponent<Image> ().sprite = btnChecked.GetComponent<Image> ().sprite;
		} else {
			saveScoreToLBCheckbox.GetComponent<Image> ().sprite = btnUnChecked.GetComponent<Image> ().sprite;
		}

		if (ApplicationModel.getQuality () == "") {
			saveScoreToLBCheckbox.GetComponent<Image> ().sprite = btnChecked.GetComponent<Image>().sprite;
		}

		resetQualityRadioBtns ();
			
		switch (ApplicationModel.getQuality ()) {
		case "Fastest":
			qualityFastest.GetComponent<Image> ().sprite = btnChecked.GetComponent<Image> ().sprite;
			break;
		case "Fast":
			qualityFast.GetComponent<Image> ().sprite = btnChecked.GetComponent<Image> ().sprite;
			break;		
		case "Simple":
			qualitySimple.GetComponent<Image> ().sprite = btnChecked.GetComponent<Image> ().sprite;
			break;
		case "Good":
			qualityGood.GetComponent<Image> ().sprite = btnChecked.GetComponent<Image> ().sprite;
			break;
		case "Beautiful":
			qualityBeautiful.GetComponent<Image> ().sprite = btnChecked.GetComponent<Image> ().sprite;
			break;
		case "Fantastic":
			qualityFantastic.GetComponent<Image> ().sprite = btnChecked.GetComponent<Image> ().sprite;
			break;
		}
	}

	void resetQualityRadioBtns() {
		qualityFastest.GetComponent<Image> ().sprite = btnUnChecked.GetComponent<Image> ().sprite;
		qualityFast.GetComponent<Image> ().sprite = btnUnChecked.GetComponent<Image> ().sprite;
		qualitySimple.GetComponent<Image> ().sprite = btnUnChecked.GetComponent<Image> ().sprite;
		qualityGood.GetComponent<Image> ().sprite = btnUnChecked.GetComponent<Image> ().sprite;
		qualityBeautiful.GetComponent<Image> ().sprite = btnUnChecked.GetComponent<Image> ().sprite;
		qualityFantastic.GetComponent<Image> ().sprite = btnUnChecked.GetComponent<Image> ().sprite;
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

	public void qualityBtnClickFastest() {
		resetQualityRadioBtns ();
		qualityFastest.GetComponent<Image> ().sprite = btnChecked.GetComponent<Image> ().sprite;
		ApplicationModel.setQuality ("Fastest");
	}

	public void qualityBtnClickFast() {
		resetQualityRadioBtns ();
		qualityFast.GetComponent<Image> ().sprite = btnChecked.GetComponent<Image> ().sprite;
		ApplicationModel.setQuality ("Fast");
	}

	public void qualityBtnClickSimple() {
		resetQualityRadioBtns ();
		qualitySimple.GetComponent<Image> ().sprite = btnChecked.GetComponent<Image> ().sprite;
		ApplicationModel.setQuality ("Simple");
	}

	public void qualityBtnClickGood() {
		resetQualityRadioBtns ();
		qualityGood.GetComponent<Image> ().sprite = btnChecked.GetComponent<Image> ().sprite;
		ApplicationModel.setQuality ("Good");
	}

	public void qualityBtnClickBeautiful() {
		resetQualityRadioBtns ();
		qualityBeautiful.GetComponent<Image> ().sprite = btnChecked.GetComponent<Image> ().sprite;
		ApplicationModel.setQuality ("Beautiful");
	}

	public void qualityBtnClickFantastic() {
		resetQualityRadioBtns ();
		qualityFantastic.GetComponent<Image> ().sprite = btnChecked.GetComponent<Image> ().sprite;
		ApplicationModel.setQuality ("Fantastic");
	}

	public void playMusicButtonClick() {
		int choice;

		if (ApplicationModel.getPlayMusicSetting () == 0) {
			choice = 1;
			playMusicCheckbox.GetComponent<Image> ().sprite = btnChecked.GetComponent<Image>().sprite;
		} else {
			choice = 0;
			playMusicCheckbox.GetComponent<Image> ().sprite = btnUnChecked.GetComponent<Image>().sprite;
		}

		ApplicationModel.setPlayMusicSetting (choice);
	}

	public void saveScoreToLBButtonClick() {
		int choice;

		if (ApplicationModel.getSaveScoreToLBSetting() == 0) {
			choice = 1;
			saveScoreToLBCheckbox.GetComponent<Image> ().sprite = btnChecked.GetComponent<Image>().sprite;
		} else {
			choice = 0;
			saveScoreToLBCheckbox.GetComponent<Image> ().sprite = btnUnChecked.GetComponent<Image>().sprite;
		}

		ApplicationModel.setSaveScoreToLBSetting (choice);
	}

	public void clearStatsBtnClick() {
		clearStatsBtn.SetActive (false);
		clearStatsConfirmBtn.SetActive (true);
		clearStatsCancelBtn.SetActive (true);
	}

	public void clearStatsConfirmBtnClick() {
		ApplicationModel.score = 0;
		ApplicationModel.clearStreak = 0;
		ApplicationModel.highClearStreak = 0;

		ApplicationModel.saveAllTimeHighScore ();
		ApplicationModel.saveAllTimeHighClearStreak ();

		//FacebookHelper.deleteMyScore (null);

		clearStatsBtn.SetActive (true);
		clearStatsConfirmBtn.SetActive (false);
		clearStatsCancelBtn.SetActive (false);
	}

	public void clearStatsCancelBtnClick() {
		clearStatsBtn.SetActive (true);
		clearStatsConfirmBtn.SetActive (false);
		clearStatsCancelBtn.SetActive (false);
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
		return btnChecked;
	}

	public GameObject getBtnUnChecked() {
		return btnUnChecked;
	}

	public GameObject getJumbotron() {
		return null;
	}
}

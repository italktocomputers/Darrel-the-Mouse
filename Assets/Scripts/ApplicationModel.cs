using UnityEngine;
using System.Collections;

public class ApplicationModel : MonoBehaviour {
	static public int score = 0;
	static public int highClearStreak = 0;
	static private int _clearStreak = 0;
	static public bool dev = false;

	static public int qualityStringToIndex(string quality) {
		switch (quality) {
		case "Fastest":
			return 0;
		case "Fast":
			return 1;
		case "Simple":
			return 2;
		case "Good":
			return 3;
		case "Beautiful":
			return 4;
		case "Fantastic":
			return 5;
		}

		return 0;
	}

	static public string getCopyrightString() {
		return "Copyright 2017, Andrew Schools.  All rights reserved.  v1.8";
	}

	static public int clearStreak {
		get {
			return ApplicationModel._clearStreak;
		}

		set {
			if (value > ApplicationModel.highClearStreak) {
				ApplicationModel.highClearStreak = value;
			}

			ApplicationModel._clearStreak = value;
		}
	}

	static public int isNotFirstTime() {
		return PlayerPrefs.GetInt ("isNotFirstTime");
	}

	static public void setIsNotFirstTime() {
		PlayerPrefs.SetInt ("isNotFirstTime", 1);
	}

	static public string getQuality() {
		return PlayerPrefs.GetString ("Quality");
	}

	static public void setQuality(string quality) {
		PlayerPrefs.SetString ("Quality", quality);
	}

	static public string getTheme() {
		return PlayerPrefs.GetString ("Theme");
	}

	static public void setTheme(string theme) {
		PlayerPrefs.SetString ("Theme", theme);
	}

	static public int getAllTimeHighScore() {
		return PlayerPrefs.GetInt ("HighScore");
	}

	static public int getAllTimeHighClearStreak() {
		return PlayerPrefs.GetInt ("HighClearStreak");
	}

	static public void saveAllTimeHighScore() {
		PlayerPrefs.SetInt ("HighScore", ApplicationModel.score);
	}

	static public void saveAllTimeHighClearStreak() {
		PlayerPrefs.SetInt ("HighClearStreak", ApplicationModel.highClearStreak);
	}

	static public int getPlayMusicSetting() {
		return PlayerPrefs.GetInt ("Setting_PlayMusic");
	}

	static public void setPlayMusicSetting(int choice) {
		PlayerPrefs.SetInt ("Setting_PlayMusic", choice);
	}

	static public int getSaveScoreToLBSetting() {
		return PlayerPrefs.GetInt ("Setting_SaveScoreToLB");
	}

	static public void setSaveScoreToLBSetting(int choice) {
		PlayerPrefs.SetInt ("Setting_SaveScoreToLB", choice);
	}
}
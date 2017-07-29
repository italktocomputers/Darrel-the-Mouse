using System;
using UnityEngine;
using UnityEngine.UI;

public static class CommonFunctions {
	public static void makeCopyrightFooter(GameObject gameobject) {
		var label = gameobject.GetComponent<Text> ();
		label.text = ApplicationModel.getCopyrightString ();
	}

	public static string getAspectRatio() {
		// 16:9 = 1.777778
		// 16:10 = 1.6
		// 3:2 = 1.5
		// 4:3 = 1.333333
		// 5:4 = 1.25
		float ratio = Camera.main.aspect;

		if (ratio >= 1.7) {
			return "16:9";
		} else if (ratio > 1.5 && ratio < 1.7) {
			return "16:10";
		} else if (ratio >= 1.5) {
			return "3:2";
		} else if (ratio >= 1.3) {
			return "4:3";
		} else {
			return "5:4";
		}
	}

	public static Vector2 getPositionOfMouse() {
		Vector2 coords = CommonFunctions.getTopLeftCoords ();

		float x = coords.x + 1.5f;
		float y = -coords.y + 1.5f;

		return new Vector2 (x, y);
	}

	public static Vector2 getTopLeftCoords() {
		string ratio = CommonFunctions.getAspectRatio ();

		if (ratio == "16:9") {
			return new Vector2(-6.2f, 3.5f);
		} else if (ratio == "16:10") {
			return new Vector2(-5.6f, 3.5f);
		} else if (ratio == "3:2") {
			return new Vector2(-5.2f, 3.5f);
		} else if (ratio == "4:3") {
			return new Vector2(-4.65f, 3.5f);
		} else {
			return new Vector2(-4.35f, 3.5f);
		}
	}

	public static void loadRandomTheme(ThemeableScene scene, string theme="") {
		string themeName;
		Sprite bg;
		Sprite hills;
		Sprite fg;
		Sprite btnBack;
		Sprite btnForward;
		Sprite btnSettings;
		Sprite btnInfo;
		Sprite btnChecked;
		Sprite btnUnChecked;
		Sprite jumbotron;

		int themeIndex;

		if (theme == "") {
			themeIndex = UnityEngine.Random.Range (0, 2);

			if (themeIndex == 1) {
				themeName = "desert";
			} else {
				themeName = "forest";
			}

			ApplicationModel.setTheme (themeName);
		} else {
			themeName  = theme;
		}

		bg = Resources.Load("themes/" + themeName + "/background", typeof(Sprite)) as Sprite;
		hills = Resources.Load("themes/" + themeName + "/hills", typeof(Sprite)) as Sprite;
		fg = Resources.Load("themes/" + themeName + "/foreground", typeof(Sprite)) as Sprite;
		btnBack = Resources.Load("themes/" + themeName + "/btn_back", typeof(Sprite)) as Sprite;
		btnForward = Resources.Load("themes/" + themeName + "/btn_forward", typeof(Sprite)) as Sprite;
		btnSettings = Resources.Load("themes/" + themeName + "/btn_settings", typeof(Sprite)) as Sprite;
		btnInfo = Resources.Load("themes/" + themeName + "/btn_info", typeof(Sprite)) as Sprite;
		btnChecked = Resources.Load("themes/" + themeName + "/btn_checked", typeof(Sprite)) as Sprite;
		btnUnChecked = Resources.Load("themes/" + themeName + "/btn_unchecked", typeof(Sprite)) as Sprite;
		jumbotron = Resources.Load("themes/" + themeName + "/jumbotron", typeof(Sprite)) as Sprite;

		scene.getBackground1().GetComponent<SpriteRenderer> ().sprite = bg;

		try {
			if (scene.getBackground2()) {
				scene.getBackground2().GetComponent<SpriteRenderer> ().sprite = bg;
			}
		} catch (System.NullReferenceException e) {
			Debug.Log (e);
		}

		scene.getHills1().GetComponent<SpriteRenderer> ().sprite = hills;

		try {
			if (scene.getHills2()) {
				scene.getHills2().GetComponent<SpriteRenderer> ().sprite = hills;
			}
		} catch (System.NullReferenceException e) {
			Debug.Log (e);
		}

		scene.getForeground1().GetComponent<SpriteRenderer> ().sprite = fg;

		try {
			if (scene.getForeground2()) {
				scene.getForeground2().GetComponent<SpriteRenderer> ().sprite = fg;
			}
		} catch (System.NullReferenceException e) {
			Debug.Log (e);
		}

		try {
			if (scene.getBtnBack ()) {
			scene.getBtnBack ().GetComponent<Image> ().sprite = btnBack;
			}
		} catch (System.NullReferenceException e) {
			Debug.Log (e);
		}

		try {
			if (scene.getBtnSettings()) {
				scene.getBtnSettings().GetComponent<Image> ().sprite = btnSettings;
			}
		} catch (System.NullReferenceException e) {
			Debug.Log (e);
		}

		try {
			if (scene.getBtnForward ()) {
				scene.getBtnForward ().GetComponent<Image> ().sprite = btnForward;
			}
		} catch (System.NullReferenceException e) {
			Debug.Log (e);
		}

		try {
			if (scene.getBtnInfo()) {
				scene.getBtnInfo().GetComponent<Image> ().sprite = btnInfo;
			}
		} catch (System.NullReferenceException e) {
			Debug.Log (e);
		}

		try {
			if (scene.getBtnChecked()) {
				scene.getBtnChecked().GetComponent<Image> ().sprite = btnChecked;
			}
		} catch (System.NullReferenceException e) {
			Debug.Log (e);
		}

		try {
			if (scene.getBtnChecked()) {
				scene.getBtnUnChecked().GetComponent<Image> ().sprite = btnUnChecked;
			}
		} catch (System.NullReferenceException e) {
			Debug.Log (e);
		}

		try {
			if (scene.getJumbotron()) {
				scene.getJumbotron().GetComponent<Image> ().sprite = jumbotron;
			}
		} catch (System.NullReferenceException e) {
			Debug.Log (e);
		}
	}
}


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Facebook.Unity;
//using UnityEngine.Advertisements;

public class StartSceneController : MonoBehaviour, ThemeableScene {
	public List<string> toolTipMsg;
	public DateTime lastTimeToolTipChanged;
	public GameObject tooltip;
	public GameObject tooltipMsgLabel;
	public GameObject FacebookBtn;
	public GameObject fly;
	public GameObject playBtn;
	public GameObject infoBtn;
	public GameObject settingsBtn;
	public GameObject copyright;
	public GameObject mouse;
	public GameObject mobileAvailable;

	public int tooltipMsgIndex;

	public GameObject[] nameLabels;
	public GameObject[] scoreLabels;

	public GameObject LBName1;
	public GameObject LBName2;
	public GameObject LBName3;
	public GameObject LBName4;
	public GameObject LBName5;
	public GameObject LBName6;
	public GameObject LBName7;
	public GameObject LBName8;
	public GameObject LBName9;
	public GameObject LBName10;
	public GameObject LBName11;
	public GameObject LBName12;
	public GameObject LBName13;
	public GameObject LBName14;
	public GameObject LBName15;
	public GameObject LBName16;
	public GameObject LBName17;
	public GameObject LBName18;
	public GameObject LBName19;
	public GameObject LBName20;

	public GameObject LBScore1;
	public GameObject LBScore2;
	public GameObject LBScore3;
	public GameObject LBScore4;
	public GameObject LBScore5;
	public GameObject LBScore6;
	public GameObject LBScore7;
	public GameObject LBScore8;
	public GameObject LBScore9;
	public GameObject LBScore10;
	public GameObject LBScore11;
	public GameObject LBScore12;
	public GameObject LBScore13;
	public GameObject LBScore14;
	public GameObject LBScore15;
	public GameObject LBScore16;
	public GameObject LBScore17;
	public GameObject LBScore18;
	public GameObject LBScore19;
	public GameObject LBScore20;

	public GameObject developmentAlert;

	public GameObject background1;
	public GameObject background2;

	public GameObject hills1;
	public GameObject hills2;

	public GameObject foreground1;
	public GameObject foreground2;

	public GameObject btnForward;
	public GameObject btnInfo;
	public GameObject btnSettings;
	
	void Start () {
		CommonFunctions.loadRandomTheme (this, "desert");
		CommonFunctions.makeCopyrightFooter (copyright);

		#if UNITY_EDITOR || UNITY_WEBGL
		fly.SetActive(true);
		#endif

		#if UNITY_WEBGL
		mobileAvailable.SetActive(true);
		#endif

		nameLabels = new GameObject[20] {
			LBName1, 
			LBName2, 
			LBName3, 
			LBName4, 
			LBName5, 
			LBName6, 
			LBName7, 
			LBName8, 
			LBName9, 
			LBName10, 
			LBName11, 
			LBName12, 
			LBName13, 
			LBName14, 
			LBName15, 
			LBName16, 
			LBName17, 
			LBName18, 
			LBName19, 
			LBName20
		};

		scoreLabels = new GameObject[20] {
			LBScore1,
			LBScore2,
			LBScore3,
			LBScore4,
			LBScore5,
			LBScore6,
			LBScore7,
			LBScore8,
			LBScore9,
			LBScore10,
			LBScore11,
			LBScore12,
			LBScore13,
			LBScore14,
			LBScore15,
			LBScore16,
			LBScore17,
			LBScore18,
			LBScore19,
			LBScore20
		};

		// Introductions and instructions.
		toolTipMsg.Add ("Howdy!");
		toolTipMsg.Add ("Ready to play?");
		toolTipMsg.Add ("Click the play button.");
		toolTipMsg.Add ("Click to jump.");
		toolTipMsg.Add ("While in the air,");
		toolTipMsg.Add ("click again to fire an arrow.");

		// Pep talk. 
		toolTipMsg.Add ("Let's do this!");
		toolTipMsg.Add ("You can do it!");
		toolTipMsg.Add ("Not to brag,");
		toolTipMsg.Add ("But I'm really good,");
		toolTipMsg.Add ("so you got nothing to worry\nabout!");

		// Random
		toolTipMsg.Add ("I'm bored!");
		toolTipMsg.Add ("Dang flies!");
		toolTipMsg.Add ("Watcha doing?");
		toolTipMsg.Add ("...");
		toolTipMsg.Add ("Did you here that?");
		toolTipMsg.Add ("Don't mind me.");
		toolTipMsg.Add ("Are you there?");
		toolTipMsg.Add ("Hello?");
		toolTipMsg.Add ("I see dead flies!");
		toolTipMsg.Add ("1000 steps!");
		toolTipMsg.Add ("Have you called your parents\nlately?");
		toolTipMsg.Add ("Got cheeze?");
		toolTipMsg.Add ("No seriously, you got any\ncheeze?");
		toolTipMsg.Add ("I feel like I'm talking to\nmyself.");

		// Albert Einstein
		toolTipMsg.Add ("Intellectuals solve problems,");
		toolTipMsg.Add ("flies prevent them.");
		toolTipMsg.Add ("The only real valuable thing,");
		toolTipMsg.Add ("is cheeze!");;
		toolTipMsg.Add ("Cheeze always attracts mice,");
		toolTipMsg.Add ("of high morality.");

		// Bubble guppies
		toolTipMsg.Add ("What time is it?");
		toolTipMsg.Add ("It's time for lunch!");

		// Wreck It Ralph.
		toolTipMsg.Add ("No cuts,");
		toolTipMsg.Add ("no buts,");
		toolTipMsg.Add ("no cheeze!");
		toolTipMsg.Add ("@;&?@#");
		toolTipMsg.Add ("I'm from the candy cheeze\ndepartment.");
		toolTipMsg.Add ("Welcome to the cheeze level.");
		toolTipMsg.Add ("I'm going Turbo!");
		toolTipMsg.Add ("I get next game!");
		toolTipMsg.Add ("Have some cheeze!");
		toolTipMsg.Add ("Let's just eat the cheeze!");
		toolTipMsg.Add ("This is getting old. Like\nmy Nana.");
		toolTipMsg.Add ("Hey yo, everybody!");
		toolTipMsg.Add ("Do you want to go down to\nTappers?");
		toolTipMsg.Add ("Turbo-Tastic!");
		toolTipMsg.Add ("Markowski!");

		// Forest gump.
		toolTipMsg.Add ("I like cheeze!");
		toolTipMsg.Add ("There's pineapple cheeze,");
		toolTipMsg.Add ("lemon cheeze,");
		toolTipMsg.Add ("coconut cheeze,");
		toolTipMsg.Add ("pepper cheeze,");
		toolTipMsg.Add ("cheeze soup,");
		toolTipMsg.Add ("cheeze stew,");
		toolTipMsg.Add ("cheeze salad,");
		toolTipMsg.Add ("cheeze and potatoes,");
		toolTipMsg.Add ("cheeze burger,");
		toolTipMsg.Add ("cheeze sandwich.");
		toolTipMsg.Add ("That, that's about it.");

		// Lord of the Flies.
		toolTipMsg.Add ("People don't help much.");
		toolTipMsg.Add ("What are we?");
		toolTipMsg.Add ("Mice?");
		toolTipMsg.Add ("Or insects?");
		toolTipMsg.Add ("Or savages?");

		// Monty Python
		toolTipMsg.Add("What... is your name?");
		toolTipMsg.Add("What... is your quest?");
		toolTipMsg.Add("What... is your favourite\ncolour?");
		toolTipMsg.Add("Cheeze is my favourite color.");

		// Die hard
		toolTipMsg.Add("Welcome to the party pal!");
		toolTipMsg.Add("Come out to the desert...");
		toolTipMsg.Add("We'll get together,");
		toolTipMsg.Add("have a few laughs...");
		toolTipMsg.Add("Just a fly in the ointment.");

		/*
		toolTipMsg.Add("");
		toolTipMsg.Add("");
		toolTipMsg.Add("");
		toolTipMsg.Add("");
		toolTipMsg.Add("");
		toolTipMsg.Add("");
		toolTipMsg.Add("");
		toolTipMsg.Add("");
		toolTipMsg.Add("");
		toolTipMsg.Add("");
		toolTipMsg.Add("");
		toolTipMsg.Add("");
		*/

		// Easter egg
		// Clicking on the mouse while this message is in
		// the tooltip will load game with red eye'd flies.
		//toolTipMsg.Add ("I like to be tickled!");

		tooltipMsgIndex = 0;

		ApplicationModel.highClearStreak = 0;

		// So I don't forget to switch out of development mode!
		if (ApplicationModel.dev == true) {
			developmentAlert.SetActive (true);
		}

		if (ApplicationModel.isNotFirstTime () == 0) {
			// They are loading this game for the first time,
			// so we will set some defaults.
			ApplicationModel.setPlayMusicSetting (1);
			ApplicationModel.setSaveScoreToLBSetting (1);
			ApplicationModel.setIsNotFirstTime ();
			ApplicationModel.setQuality ("Good");
		}
			
		if (ApplicationModel.getSaveScoreToLBSetting () == 1) {
			if (FB.IsInitialized) {
				FacebookReady ();
			} else {
				FB.Init (FacebookReady);
			}
		}

		QualitySettings.SetQualityLevel (ApplicationModel.qualityStringToIndex(ApplicationModel.getQuality()));

		SpriteRenderer m = mouse.GetComponent<SpriteRenderer> ();
		m.transform.position = CommonFunctions.getPositionOfMouse ();
	}

	void Update () {
		DateTime now = DateTime.Now;
		TimeSpan timespan = (now - lastTimeToolTipChanged).Duration();

		#if UNITY_EDITOR || UNITY_WEBGL
			// Control the fly with the mouse.
			Vector3 pointer = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			fly.transform.position = pointer;
		#endif

		if (timespan.TotalSeconds >= 5) {
			TextMesh tooltipMsg = tooltip.GetComponent<TextMesh> ();

			if (tooltipMsgIndex >= toolTipMsg.Count) {
				tooltipMsgIndex = 0;
			}

			tooltipMsg.text = toolTipMsg[tooltipMsgIndex];

			// Update time
			lastTimeToolTipChanged = DateTime.Now;
			tooltipMsgIndex++;
		}
	}

	public void onStartGameButtonClick() {
		SceneManager.LoadScene ("GameScene");
	}

	public void onFacebookButtonClick() {
		
	}

	public void onInfoButtonClick() {
		SceneManager.LoadScene ("InfoScene");
	}

	public void onSettingsButtonClick() {
		SceneManager.LoadScene ("SettingsScene");
	}
		
	public void populateLeaderBoard(Facebook.Unity.IGraphResult result) {
		if (String.IsNullOrEmpty(result.Error) && !result.Cancelled) {
			var dataList = result.ResultDictionary["data"] as List<object>;

			for (int i=0; i< dataList.Count; i++) {
				var labelName = nameLabels [i].GetComponent<Text> ();
				var labelScore = scoreLabels [i].GetComponent<Text> ();

				var dataDict = dataList[i] as Dictionary<string, object>;

				long score = (long)dataDict["score"];
				var user = dataDict["user"] as Dictionary<string, object>;

				labelName.text = user["name"] as string;
				labelScore.text = score.ToString ();
			}
		}
	}

	public void syncWithFB(Facebook.Unity.IGraphResult result) {
		if (String.IsNullOrEmpty(result.Error) && !result.Cancelled) {
			var dataList = result.ResultDictionary["data"] as List<object>;

			if (dataList != null && dataList.Count > 0) {
				var dataDict = dataList [0] as Dictionary<string, object>;
				long FBScore = (long)dataDict ["score"];

				if (ApplicationModel.getAllTimeHighScore () > FBScore) {
					// They have a local score that is better than what they
					// have saved in FB so we will update their FB score.
					FacebookHelper.SaveScore (ApplicationModel.getAllTimeHighScore (), null);
				} else {
					// They have a higher FB score so we will update the local score
					// to reflect what they have in FB.
					ApplicationModel.score = (int)FBScore;
					ApplicationModel.saveAllTimeHighScore ();
				}
			}
		}
	}

	public void FacebookReady() {
		FacebookHelper.loginToFB (delegate(Facebook.Unity.ILoginResult result) {
			if (ApplicationModel.getSaveScoreToLBSetting() == 1) {
				FacebookHelper.getMyScore(syncWithFB);
			}

			FacebookHelper.getScores (populateLeaderBoard);
		});
	}

	public GameObject getBackground1() {
		return background1;
	}

	public GameObject getBackground2() {
		return background2;
	}

	public GameObject getHills1() {
		return hills1;
	}

	public GameObject getHills2() {
		return hills2;
	}

	public GameObject getForeground1() {
		return foreground1;
	}

	public GameObject getForeground2() {
		return foreground2;
	}

	public GameObject getBtnForward() {
		return btnForward;
	}

	public GameObject getBtnBack() {
		return null;
	}

	public GameObject getBtnSettings() {
		return btnSettings;
	}

	public GameObject getBtnInfo() {
		return btnInfo;
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

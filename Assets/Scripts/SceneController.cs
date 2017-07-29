using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Facebook.Unity;
//using UnityEngine.Advertisements;

public class SceneController : MonoBehaviour, ThemeableScene {
	public float minIntervalToAddAnt;
	public float maxIntervalToAddAnt;
	public DateTime lastTimeAntAdded;
	public float intervalToAddAnt;

	public float minIntervalToAddFly;
	public float maxIntervalToAddFly;
	public DateTime lastTimeFlyAdded;
	public float intervalToAddFly;

	public float minIntervalToAddBomb;
	public float maxIntervalToAddBomb;
	public DateTime lastTimeBombAdded;
	public float intervalToAddBomb;

	public float minIntervalToAddQuiver;
	public float maxIntervalToAddQuiver;
	public DateTime lastTimeQuiverAdded;
	public float intervalToAddQuiver;

	public float minIntervalToAddDustStorm;
	public float maxIntervalToAddDustStorm;
	public DateTime lastTimeDustStormAdded;
	public float intervalToAddDustStorm;

	public float minIntervalToAddCactus;
	public float maxIntervalToAddCactus;
	public DateTime lastTimeCactusAdded;
	public float intervalToAddCactus;

	public GameObject livesLabel;
	public GameObject scoreboardLabel;
	public GameObject arrowLabel;
	public GameObject clearStreakLabel;
	public GameObject jumbotron;
	public GameObject textMessageLabel;
	public GameObject panel;
	public GameObject gameCanvas;
	public GameObject gameOverCanvas;
	public GameObject playBtn;
	public GameObject fbBtn;
	public GameObject uiArrow;

	public GameObject thisGameScore;
	public GameObject bestGameScore;
	public GameObject communityGameScore;

	public GameObject thisGameClearStreak;
	public GameObject bestGameClearStreak;
	public GameObject communityGameClearStreak;

	public GameObject newHighScore;
	public GameObject newHighClearStreak;
	public GameObject communityNewHighScore;
	public GameObject communityNewHighClearStreak;

	public GameObject flyPointer;

	public GameObject msg1;
	public GameObject msg2;
	public GameObject msg3;

	public DateTime lastKill;
	
	public bool isGameOver;
	public bool isGamePaused;
	public bool isMusicEnabled;

	public bool highScoreTextMessageShown;
	public bool highClearStreakTextMessageShown;

	public int level;
	public float changeLevelEvery;
	public DateTime lastTimeLevelChanged;

	public GameObject Mouse;
	public Image jumbotronImage;

	public DateTime lastTimeTextMessageShown;
	public Queue textMessageQueue;

	public GameObject background1;
	public GameObject background2;

	public GameObject hills1;
	public GameObject hills2;

	public GameObject foreground1;
	public GameObject foreground2;

	public GameObject btnBack;
	public GameObject btnForward;
	public GameObject btnInfo;
	public GameObject btnSettings;
	public GameObject btnChecked;
	public GameObject btnUnChecked;

	public DateTime gameStart;
	public DateTime gameEnd;
	
	void Start () {
		CommonFunctions.loadRandomTheme (this, "desert");
		AudioController.init ();

		ApplicationModel.score = 0;
		ApplicationModel.clearStreak = 0;

		if (ApplicationModel.getPlayMusicSetting () == 1) {
			AudioController.playMusic ();
		}

		textMessageQueue = new Queue();
		lastTimeTextMessageShown = DateTime.Now;
		jumbotronImage = jumbotron.GetComponent<Image> ();
		jumbotronImage.enabled = false;

		highScoreTextMessageShown = false;
		highClearStreakTextMessageShown = false;

		minIntervalToAddAnt = 5.0f;
		maxIntervalToAddAnt = 10.0f;
		minIntervalToAddFly = 2.0f;
		maxIntervalToAddFly = 4.0f;
		changeLevelEvery = 30f;

		lastTimeAntAdded = DateTime.Now;
		lastTimeFlyAdded = DateTime.Now;

		minIntervalToAddBomb = 45.0f;
		maxIntervalToAddBomb = 60.0f;
		lastTimeBombAdded = DateTime.Now;
		intervalToAddBomb = minIntervalToAddBomb;

		minIntervalToAddQuiver = 5.0f;
		maxIntervalToAddQuiver = 10.0f;
		lastTimeQuiverAdded = DateTime.Now;
		intervalToAddQuiver = minIntervalToAddQuiver;

		minIntervalToAddDustStorm = 25.0f;
		maxIntervalToAddDustStorm = 60.0f;
		lastTimeDustStormAdded = DateTime.Now;
		intervalToAddDustStorm = minIntervalToAddDustStorm;

		minIntervalToAddCactus = 15.0f;
		maxIntervalToAddCactus = 30.0f;
		//lastTimeCactusAdded = DateTime.Now;
		intervalToAddCactus = minIntervalToAddCactus;

		lastTimeLevelChanged = DateTime.Now;

		SpriteRenderer m = Mouse.GetComponent<SpriteRenderer> ();
		m.transform.position = CommonFunctions.getPositionOfMouse ();

		#if UNITY_EDITOR || UNITY_WEBGL
		flyPointer.SetActive(true);
		#endif

		updateUI ();

		gameStart = DateTime.Now;
	}

	private void updateUI() {
		Vector3 coords = CommonFunctions.getTopLeftCoords ();

		uiArrow.transform.position = new Vector2 (coords.x+0.5f, coords.y-0.2f);
		arrowLabel.transform.position = new Vector2 (coords.x+1.2f, coords.y-0.2f);
		livesLabel.transform.position = new Vector2 (coords.x+2.0f, coords.y-0.2f);
		clearStreakLabel.transform.position = new Vector2 (coords.x+4.0f, coords.y-0.2f);
		scoreboardLabel.transform.position = new Vector2 (-coords.x-0.3f, coords.y-0.2f);
	}

	void Update () {
		if (!isGameOver) {
			DateTime now = DateTime.Now;

			TimeSpan level_timespan = (now - lastTimeLevelChanged).Duration ();
			if (level_timespan.TotalSeconds >= changeLevelEvery) {
				// For every x seconds, we increase the amount of flies that are added 
				// to the scene.  The Mouse must die!
				lastTimeLevelChanged = DateTime.Now;

				UpdateLifeLabel (++Mouse.GetComponent<MouseController> ().lives);

				textMessageQueue.Enqueue ("+1 life added, but you'll need it!");

				minIntervalToAddFly--;
				maxIntervalToAddFly--;

				if (minIntervalToAddFly < 1.0f) {
					minIntervalToAddFly = 0.5f;
				}

				if (maxIntervalToAddFly < 1.0f) {
					maxIntervalToAddFly = 0.5f;
				}

				minIntervalToAddBomb -= 5.0f;
				maxIntervalToAddBomb -= 5.0f;

				if (minIntervalToAddBomb < 5.0f) {
					minIntervalToAddBomb = 5f;
				}

				if (maxIntervalToAddBomb < 10.0f) {
					maxIntervalToAddBomb = 10.0f;
				}

			}

			TimeSpan ant_timespan = (now - lastTimeAntAdded).Duration ();
			if (ant_timespan.TotalSeconds >= intervalToAddAnt) {
				intervalToAddAnt = UnityEngine.Random.Range (minIntervalToAddAnt, maxIntervalToAddAnt);
				addAnt ();
			}

			TimeSpan fly_timespan = (now - lastTimeFlyAdded).Duration ();
			if (fly_timespan.TotalSeconds >= intervalToAddFly) {
				intervalToAddFly = UnityEngine.Random.Range (minIntervalToAddFly, maxIntervalToAddFly);
				addFly ();
			}

			TimeSpan bomb_timespan = (now - lastTimeBombAdded).Duration ();
			if (bomb_timespan.TotalSeconds >= intervalToAddBomb) {
				intervalToAddBomb = UnityEngine.Random.Range (minIntervalToAddBomb, maxIntervalToAddBomb);
				addBomb ();
			}

			TimeSpan Quiver_timespan = (now - lastTimeQuiverAdded).Duration ();
			if (Quiver_timespan.TotalSeconds >= intervalToAddQuiver) {
				intervalToAddQuiver = UnityEngine.Random.Range (minIntervalToAddQuiver, maxIntervalToAddQuiver);
				addQuiver ();
			}

			TimeSpan DustStorm_timespan = (now - lastTimeDustStormAdded).Duration ();
			if (DustStorm_timespan.TotalSeconds >= intervalToAddDustStorm) {
				intervalToAddDustStorm = UnityEngine.Random.Range (minIntervalToAddDustStorm, maxIntervalToAddDustStorm);
				addDustStorm ();
			}

			TimeSpan Cactus_timespan = (now - lastTimeCactusAdded).Duration ();
			if (Cactus_timespan.TotalSeconds >= intervalToAddCactus) {
				intervalToAddCactus = UnityEngine.Random.Range (minIntervalToAddCactus, maxIntervalToAddCactus);
				addCactus ();
			}

			// Show a message (if any)
			TimeSpan lastTextMessageShown_timespan = (now - lastTimeTextMessageShown).Duration ();

			if (lastTextMessageShown_timespan.TotalSeconds >= 2) {
				Text label = textMessageLabel.GetComponent<Text> ();

				if (textMessageQueue.Count > 0) {
					// Shown next message
					jumbotronImage.enabled = true;
					label.text = textMessageQueue.Peek ().ToString ();
					textMessageQueue.Dequeue ();
					lastTimeTextMessageShown = DateTime.Now;
				} else {
					// Clear
					label.text = "";
					jumbotronImage.enabled = false;
				}
			}

			if (ApplicationModel.score > ApplicationModel.getAllTimeHighScore() && highScoreTextMessageShown == false) {
				textMessageQueue.Enqueue ("Your best score!!!");
				highScoreTextMessageShown = true;
			}

			if (ApplicationModel.clearStreak > ApplicationModel.getAllTimeHighClearStreak() && highClearStreakTextMessageShown == false) {
				textMessageQueue.Enqueue ("Your best clear streak!!!");
				highClearStreakTextMessageShown = true;
			}
		} else {
			#if UNITY_EDITOR || UNITY_WEBGL
				// Control the fly with the mouse.
				Vector3 pointer = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				flyPointer.transform.position = pointer;
			#endif
		}
	}

	private void addAnt() {
		lastTimeAntAdded = DateTime.Now;

		Vector2 coords = CommonFunctions.getTopLeftCoords ();

		// Load sprite from resource folder
		Sprite backSprite = Resources.Load("blackant", typeof(Sprite)) as Sprite;
		GameObject obj = new GameObject("moving ant");
		obj.tag = "BlackAnt";
		obj.transform.position = new Vector2 (-coords.x+1.0f, -coords.y+1.12f);
		obj.transform.localScale = new Vector2 (0.8f, 0.8f);
		obj.AddComponent<BlackAntController> ();

		SpriteRenderer renderer = obj.AddComponent<SpriteRenderer>();
		renderer.sprite = backSprite;
		renderer.sortingOrder = 2;

		BoxCollider2D bc = obj.AddComponent<BoxCollider2D>();
		bc.isTrigger = true;
		bc.size = new Vector2 (1, 1);

		Rigidbody2D rb = obj.AddComponent<Rigidbody2D>();
		rb.gravityScale = 0;

		Animator animator = obj.AddComponent<Animator>();
		RuntimeAnimatorController contr = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(
			Resources.Load("Animations/BlackAntAnimationController", typeof(RuntimeAnimatorController ))
		);

		animator.runtimeAnimatorController = contr;
	}

	private void addFly() {
		lastTimeFlyAdded = DateTime.Now;

		Vector2 coords = CommonFunctions.getTopLeftCoords ();

		// Starting point
		float y = UnityEngine.Random.Range (coords.y, -coords.y);
		
		// Load sprite from resource folder
		Sprite backSprite = Resources.Load("fly", typeof(Sprite)) as Sprite;
		GameObject obj = new GameObject("moving fly");
		obj.tag = "Fly";
		obj.transform.position = new Vector2 (-coords.x+1.0f, y);
		obj.AddComponent<FlyController>();
		
		SpriteRenderer renderer = obj.AddComponent<SpriteRenderer>();
		renderer.sprite = backSprite;
		renderer.sortingOrder = 2;
		
		BoxCollider2D bc = obj.AddComponent<BoxCollider2D>();
		bc.isTrigger = true;
		bc.size = new Vector2 (0.5f, 0.8f);

		Rigidbody2D rb = obj.AddComponent<Rigidbody2D>();
		rb.gravityScale = 0;
		
		Animator animator = obj.AddComponent<Animator>();

		RuntimeAnimatorController contr = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(
			Resources.Load("Animations/FlyAnimationController", typeof(RuntimeAnimatorController ))
		);

		animator.runtimeAnimatorController = contr;
	}

	private void addBomb() {
		lastTimeBombAdded = DateTime.Now;

		Vector2 coords = CommonFunctions.getTopLeftCoords ();

		float y = UnityEngine.Random.Range (coords.y-2.0f, -coords.y+2.0f);
		
		// Load sprite from resource folder
		Sprite backSprite = Resources.Load("bomb", typeof(Sprite)) as Sprite;
		GameObject obj = new GameObject("bomb");
		obj.tag = "Bomb";
		obj.transform.position = new Vector2 (-coords.x+2.0f, y);
		obj.AddComponent<BombController>();
		
		SpriteRenderer renderer = obj.AddComponent<SpriteRenderer>();
		renderer.sprite = backSprite;
		renderer.sortingOrder = 2;
		
		BoxCollider2D bc = obj.AddComponent<BoxCollider2D>();
		bc.isTrigger = true;
		bc.size = new Vector2 (1, 1);
		
		Rigidbody2D rb = obj.AddComponent<Rigidbody2D>();
		rb.gravityScale = 0;

		Animator animator = obj.AddComponent<Animator>();

		RuntimeAnimatorController contr = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(
			Resources.Load("Animations/BombAnimationController", typeof(RuntimeAnimatorController ))
		);

		animator.runtimeAnimatorController = contr;
	}

	private void addQuiver() {
		lastTimeQuiverAdded = DateTime.Now;
		
		Vector2 coords = CommonFunctions.getTopLeftCoords ();

		float y = UnityEngine.Random.Range (coords.y-1.0f, -coords.y+2.0f);
		
		// Load sprite from resource folder
		Sprite backSprite = Resources.Load("arrow", typeof(Sprite)) as Sprite;
		GameObject obj = new GameObject("quiver");
		obj.tag = "Quiver";
		obj.transform.position = new Vector2 (-coords.x+2.0f, y);
		obj.AddComponent<QuiverController>();
		
		SpriteRenderer renderer = obj.AddComponent<SpriteRenderer>();
		renderer.sprite = backSprite;
		renderer.sortingOrder = 2;
		
		BoxCollider2D bc = obj.AddComponent<BoxCollider2D>();
		bc.isTrigger = true;
		
		Rigidbody2D rb = obj.AddComponent<Rigidbody2D>();
		rb.gravityScale = 0;
	}

	private void addDustStorm() {
		lastTimeDustStormAdded = DateTime.Now;

		/*
		Vector2 coords = CommonFunctions.getTopLeftCoords ();

		float y = UnityEngine.Random.Range (coords.y, -coords.y);

		// Load sprite from resource folder
		Sprite backSprite = Resources.Load("duststorm", typeof(Sprite)) as Sprite;
		GameObject obj = new GameObject("duststorm");
		obj.tag = "DustStorm";
		obj.transform.position = new Vector2 (-coords.x+2.0f, y);
		obj.AddComponent<DustStormController>();

		SpriteRenderer renderer = obj.AddComponent<SpriteRenderer>();
		renderer.sprite = backSprite;
		renderer.sortingOrder = 3;
		*/

		addTumbleweed ();
	}

	private void addTumbleweed() {
		Vector2 coords = CommonFunctions.getTopLeftCoords ();

		float y = UnityEngine.Random.Range (coords.y, -coords.y);

		// Load sprite from resource folder
		Sprite backSprite = Resources.Load("tumbleweed", typeof(Sprite)) as Sprite;
		GameObject obj = new GameObject("tumbleweed");
		obj.tag = "Tumbleweed";
		obj.transform.position = new Vector2 (-coords.x+2.0f, -2.31f);
		obj.AddComponent<TumbleweedController>();

		BoxCollider2D bc = obj.AddComponent<BoxCollider2D>();
		bc.isTrigger = true;
		bc.size = new Vector2 (0.5f, 0.5f);

		SpriteRenderer renderer = obj.AddComponent<SpriteRenderer>();
		renderer.sprite = backSprite;
		renderer.sortingOrder = 3;
	}

	private void addCactus() {
		lastTimeCactusAdded = DateTime.Now;

		Vector2 coords = CommonFunctions.getTopLeftCoords ();
		//float randSize = UnityEngine.Random.Range (0.2f, 0.7f);

		// Load sprite from resource folder
		Sprite backSprite = Resources.Load("cactus", typeof(Sprite)) as Sprite;
		GameObject obj = new GameObject("cactus");
		obj.tag = "Cactus";
		obj.transform.position = new Vector2 (-coords.x+1.0f, -2.08f);
		obj.AddComponent<CactusController>();
		//obj.transform.localScale = new Vector2 (randSize, randSize);

		SpriteRenderer renderer = obj.AddComponent<SpriteRenderer>();
		renderer.sprite = backSprite;
		renderer.sortingOrder = 3;
		renderer.sortingLayerName = "Background";
	}

	public void gameOver() {
		gameEnd = DateTime.Now;

		gameCanvas.SetActive (false);
		gameOverCanvas.SetActive (true);
		flyPointer.SetActive (true);

		// Randomly change gameover title.
		int x = UnityEngine.Random.Range (0, 2);

		if (x == 0) {
			msg1.SetActive (true);
		} else if (x == 1) {
			msg2.SetActive (true);
		} else if (x == 2) {
			msg3.SetActive (true);
		}

		if (ApplicationModel.score > ApplicationModel.getAllTimeHighScore()) {
			ApplicationModel.saveAllTimeHighScore();
			newHighScore.SetActive (true);

			if (ApplicationModel.getSaveScoreToLBSetting() == 1) {
				FacebookHelper.SaveScore (ApplicationModel.score, null);
			}
		}

		if (ApplicationModel.highClearStreak > ApplicationModel.getAllTimeHighClearStreak()) {
			ApplicationModel.saveAllTimeHighClearStreak ();
			newHighClearStreak.SetActive (true);
		}
			
		// Update labels
		Text thisGameScoreLabel = thisGameScore.GetComponent<Text>();
		thisGameScoreLabel.text = ApplicationModel.score.ToString();

		Text bestGameScoreLabel = bestGameScore.GetComponent<Text>();
		bestGameScoreLabel.text = ApplicationModel.getAllTimeHighScore ().ToString();

		Text thisGameClearStreakLabel = thisGameClearStreak.GetComponent<Text>();
		thisGameClearStreakLabel.text = ApplicationModel.highClearStreak.ToString ();

		Text bestGameClearStreakLabel = bestGameClearStreak.GetComponent<Text>();
		bestGameClearStreakLabel.text = ApplicationModel.getAllTimeHighClearStreak ().ToString();

		if (ApplicationModel.getSaveScoreToLBSetting () == 1) {
			FacebookHelper.getScores (populateCommunityScore);
		}
	}

	public void goBackToStart() {
		#if UNITY_ADS
		TimeSpan game_timespan = (gameEnd - gameStart).Duration ();
		if (game_timespan.TotalSeconds >= 45) {
			if (Advertisement.IsReady()) {
				Advertisement.Show();
			}
		}
		#endif

		SceneManager.LoadScene ("StartScene");
	}

	public void shareToFacebook() {
		FacebookHelper.shareCompletedGame (ApplicationModel.score, ApplicationModel.highClearStreak, null);
	}

	public void UpdateLifeLabel(int lives) {
		Text label = livesLabel.GetComponent<Text>();
		label.text = "Life: " + lives.ToString ();
	}

	public void UpdateArrowLabel(int num) {
		Text label = arrowLabel.GetComponent<Text>();
		label.text = num.ToString ();
	}

	public void UpdateScoreboardLabel() {
		Text label = scoreboardLabel.GetComponent<Text>();
		int numZeros = 3 - ApplicationModel.score.ToString ().Length;
		label.text = "";

		for (int i=0; i<numZeros; i++) {
			label.text += '0';
		}

		label.text += ApplicationModel.score.ToString();
	}

	public void UpdateClearStreakLabel() {
		Text label = clearStreakLabel.GetComponent<Text>();

		if (ApplicationModel.clearStreak >= 5) {
			label.text = "Clear Streak: " + ApplicationModel.clearStreak.ToString ();
		} else {
			label.text = "";
		}

		if (ApplicationModel.clearStreak > 0 && ApplicationModel.clearStreak % 20 == 0) {
			textMessageQueue.Enqueue (ApplicationModel.clearStreak.ToString() + " in a row!  What a Warrior!");
		}
	}

	public void destroyAllEnemies() {
		GameObject[] fly_array =  GameObject.FindGameObjectsWithTag ("Fly");
		
		for(int i = 0 ; i < fly_array.Length; i++) {
			FlyController controller = fly_array[i].GetComponent<FlyController>();
			controller.kill ();
		}

		GameObject[] ant_array =  GameObject.FindGameObjectsWithTag ("BlackAnt");
		
		for(int i = 0 ; i < ant_array.Length; i++) {
			BlackAntController controller = ant_array[i].GetComponent<BlackAntController>();
			controller.kill ();
		}
	}

	public void removeAllItems() {
		GameObject[] fly_array =  GameObject.FindGameObjectsWithTag ("Fly");

		for(int i = 0 ; i < fly_array.Length; i++) {
			Destroy(fly_array[i]);
		}

		GameObject[] ant_array =  GameObject.FindGameObjectsWithTag ("BlackAnt");

		for(int i = 0 ; i < ant_array.Length; i++) {
			Destroy(ant_array[i]);
		}

		GameObject[] quiver_array =  GameObject.FindGameObjectsWithTag ("Quiver");

		for(int i = 0 ; i < quiver_array.Length; i++) {
			Destroy(quiver_array[i]);
		}

		GameObject[] bombs_array =  GameObject.FindGameObjectsWithTag ("Bomb");

		for(int i = 0 ; i < bombs_array.Length; i++) {
			Destroy(bombs_array[i]);
		}

		GameObject[] duststorm_array =  GameObject.FindGameObjectsWithTag ("DustStorm");

		for(int i = 0 ; i < duststorm_array.Length; i++) {
			Destroy(duststorm_array[i]);
		}

		GameObject[] tumbleweed_array =  GameObject.FindGameObjectsWithTag ("Tumbleweed");

		for(int i = 0 ; i < tumbleweed_array.Length; i++) {
			Destroy(tumbleweed_array[i]);
		}

		GameObject[] cactus_array =  GameObject.FindGameObjectsWithTag ("Cactus");

		for(int i = 0 ; i < cactus_array.Length; i++) {
			Destroy(cactus_array[i]);
		}
	}

	public void populateCommunityScore(Facebook.Unity.IGraphResult result) {
		if (String.IsNullOrEmpty(result.Error) && !result.Cancelled) {
			var dataList = result.ResultDictionary["data"] as List<object>;
			var labelName = communityGameScore.GetComponent<Text> ();
			var dataDict = dataList[0] as Dictionary<string, object>;
			long score = (long)dataDict["score"];
			labelName.text = score.ToString ();
		}
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
		return jumbotron;
	}
}

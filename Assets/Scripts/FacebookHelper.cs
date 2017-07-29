using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;

public static class FacebookHelper {
	public static void shareCompletedGame(int score, int clearStreak, Facebook.Unity.FacebookDelegate<Facebook.Unity.IShareResult> callback) {
		FB.FeedShare (
			"",
			new Uri ("https://apps.facebook.com/darrel-the-mouse/"),
			"Completed Game", 
			"Play Darrel the Mouse and the Pesky Flies for FREE!", 
			"Score: " + score.ToString() + " || Clear Streak: " + clearStreak.ToString(), 
			new Uri ("https://s3.amazonaws.com/darrel-the-mouse/mouse.png"),
			"",
			callback
		);
	}

	public static void SaveScore(int _score, Facebook.Unity.FacebookDelegate<Facebook.Unity.IGraphResult> callback) {
		var _query = new Dictionary<string, string>();
		_query ["score"] = _score.ToString ();

		FB.API(
			"/me/scores", 
			Facebook.Unity.HttpMethod.POST, 
			callback, 
			_query
		);
	}

	public static void getScores(Facebook.Unity.FacebookDelegate<Facebook.Unity.IGraphResult> callback) {
		var _query = new Dictionary<string, string>();

		FB.API(
			"/" + FB.AppId + "/scores?fields=score&limit=10", 
			Facebook.Unity.HttpMethod.GET, 
			callback, 
			_query
		);
	}

	public static void getMyScore(Facebook.Unity.FacebookDelegate<Facebook.Unity.IGraphResult> callback) {
		var _query = new Dictionary<string, string>();

		FB.API(
			"/me/scores?fields=score", 
			Facebook.Unity.HttpMethod.GET, 
			callback, 
			_query
		);
	}

	public static void deleteMyScore(Facebook.Unity.FacebookDelegate<Facebook.Unity.IGraphResult> callback) {
		var _query = new Dictionary<string, string>();

		FB.API(
			"/me/scores", 
			Facebook.Unity.HttpMethod.DELETE, 
			callback, 
			_query
		);
	}

	public static void loginToFB(Facebook.Unity.FacebookDelegate<Facebook.Unity.ILoginResult> callback) {
		if (!FB.IsLoggedIn) {
			//
			// NOTE: It doesn't seem you can ask for more than one permission at a time on Android...
			//
			// FROM Facebook:
			// 
			// Both the Scores API and Achievements API are only available for apps that are categorized as Games.
			// Because these API let developers write information about player game state to the Graph API, they 
			// both require publish_actions permissions, which is subject to Login Review. To retrieve Score and 
			// Achievement information about a player's friends, both the player and their friends need to grant 
			// the user_friends permission.
			//
			//
			FB.LogInWithPublishPermissions (
				new List<string> (){ "publish_actions", "user_friends"}, 
				callback
			);
		} else {
			callback (null);
		}
	}
}
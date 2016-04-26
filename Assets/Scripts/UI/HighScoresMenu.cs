using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HighScoresMenu : Menu {

	public UserProfile profile;
	public Text scoreText;
	private List<int> scores;

	// Use this for initialization
	void Start () {
		if (profile != null) {
			scores = profile.GetScores ();
		} else {
			Debug.Log ("Could not load scores");
		}

		string scoreString = "";

		for (int i = 0; i < 5; i++) {
			scoreString += "" + (i + 1) + ": " + scores [i] + "\n";
		}

		scoreText.text = scoreString;
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HighScoresMenu : Menu {

	public UserProfile profile;		//Reference to our UserProfile
	public Text scoreText;			//Reference to the textField that displays our score

	private List<int> scores;		//Privately held list of scores

	// Use this for initialization
	void Start () {
		//Ensure that we have scores in the first place
		if (profile != null) {
			scores = profile.GetScores ();
		} else {
			Debug.Log ("Could not load scores");
		}

		//The string that we will print in our scoreText
		string scoreString = "";

		for (int i = 0; i < 5; i++) {
			scoreString += "" + (i + 1) + ": " + scores [i] + "\n";
		}

		scoreText.text = scoreString;
	}
}

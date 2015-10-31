using UnityEngine;
using System.Collections;

/**
 * The UserProfile class is responsible for handeling all scene and
 * session persistent data.
 * 
 * This includes but is not limited to: player names, volume preferences
 * and scores.
 */
public class UserProfile : MonoBehaviour {

	/*
	 * Clears a specific key from the player prefs, 
	 * if the key is null then the entire PlayerPrefs will be cleared.
	 */ 
	public void Clear (string key) {
		if (key == null) {
			PlayerPrefs.DeleteAll ();
		} else {
			if (PlayerPrefs.HasKey(key)) {
				PlayerPrefs.DeleteKey(key);
			}
		} 
	}

	public void SetName(string name) {
		PlayerPrefs.SetString ("name", name);
	}

	public void SetEffectsVolume(float value) {
		PlayerPrefs.SetFloat ("effectsVolume", value);
	}

	public void SetMusicVolume(float value) {
		PlayerPrefs.SetFloat ("musicVolume", value);
	}

	public string GetName () {
		return PlayerPrefs.GetString ("name");
	}

	public float GetEffectsVolume () {
		return PlayerPrefs.GetFloat ("effectsVolume");
	}

	public float GetMusicVolume () {
		return PlayerPrefs.GetFloat ("musicVolume");
	}
}

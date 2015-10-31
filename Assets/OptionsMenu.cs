using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsMenu : Menu {

	public UserProfile profile;
	public InputField nameField;
	public Slider musicValue;
	public Slider effectValue;

	// Use this for initialization
	void Start () {
		if (profile.GetName () != null) {
			nameField.text = profile.GetName();
		}
		if (profile.GetMusicVolume() != null) {
			musicValue.value = profile.GetMusicVolume();
		}
		if (profile.GetEffectsVolume() != null) {
			effectValue.value = profile.GetEffectsVolume();
		}
	}
}

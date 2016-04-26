using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsMenu : Menu {

	public UserProfile profile;		//Reference to our UserProfile
	public InputField nameField;	//Reference to the name input field
	public Slider musicValue;		//Slider that controls the music volume
	public Slider effectValue;		//Slider that controls the effect volume

	void Start () {
		//If any values have already been set then display them as such.
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

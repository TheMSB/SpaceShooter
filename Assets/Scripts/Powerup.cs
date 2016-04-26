using UnityEngine;
using System.Collections;

/**
 * Generic powerup class that restores a specified amount of health
 * and upgrades the player weapon.
 */
public class Powerup : MonoBehaviour {

	public int scoreValue;					//The amount of score this object is worth
	public int healValue;					//The amount of armor to restore
	private GameController gameController;	//Reference to our gamecontroller
	
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController> ();
		} else {
			Debug.Log ("Cannot find 'GameController' script!");
		}
	}
	
	void OnTriggerEnter(Collider other) 
	{
		//Ensure only the player can pick us up
		if (other.tag == "Player") {
			Destroy(gameObject);
			PlayerController player = other.gameObject.GetComponent <PlayerController> ();

			player.Heal (healValue);
			player.levelUp ();

			gameController.UpdateArmor ();
			gameController.AddScore (scoreValue);
		}
	}
}

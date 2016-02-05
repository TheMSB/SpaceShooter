using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {

	public int scoreValue;
	public int healValue;
	private GameController gameController;
	
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
		if (other.tag == "Player") {
			PlayerController player = other.gameObject.GetComponent <PlayerController> ();
			player.Heal (healValue);
			player.setWeapon(1);
			gameController.UpdateArmor ();
			gameController.AddScore (scoreValue);

			Destroy(gameObject);
		}
	}
}

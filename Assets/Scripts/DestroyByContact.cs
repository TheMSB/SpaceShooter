using UnityEngine;
using System.Collections;

/**
 * This class dictates how colliding objects should inflict damage upon one another.
 **/
public class DestroyByContact : MonoBehaviour
{

	public GameObject explosion;		//The explosion we should display when colliding
	public GameObject enemyExplosion;	//The explosion we should display when an enemy is destroyed upon collision
	public GameObject playerExplosion;	//The explosion we should display when the player is destroyed upon collision
	public int scoreValue;				//How many points should be awarded when this object is destroyed
	public int impactDamage;			//How many points of damage should be inflicted upon impact

	private GameController gameController;	//Reference to our gameController
	private Transform trans;				//Reference to this objects transform

	void Start ()
	{
		trans = GetComponent<Transform> ();

		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController> ();
		} else {
			Debug.Log ("Cannot find 'GameController' script!");
		}
	}

	void OnTriggerEnter (Collider other)
	{
		//We ignore collisions between enemies, powerups and the boundry field
		if (other.tag != "Boundary" && other.tag != "Enemy" && other.tag != "Powerup") {
			//If we have an impact explosion then render it
			if (explosion != null) {
				Instantiate (explosion, trans.position, trans.rotation);
			}


			if (other.tag == "Player") {
				PlayerController player = other.gameObject.GetComponent <PlayerController> ();
				player.Damage (impactDamage);
				gameController.UpdateArmor ();

				//If the player has no armor left then we destroy him and end the game
				if (player.stats.armor == 0) {
					Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
					Destroy (other.gameObject);
					gameController.GameOver ();
				}
			} else {
				Destroy (other.gameObject);
			}

			//If this colliding object is an enemy instance we also inflict damage
			if (this.tag == "Enemy") {
				EnemyController enemy = this.gameObject.GetComponent <EnemyController> ();

				//Check if we have an enemy controller otherwise just destroy us
				if (enemy != null) {
					enemy.Damage (10);
					Instantiate (enemyExplosion, this.transform.position, this.transform.rotation);

					//If we have no armor left then destroy us
					if (enemy.stats.armor == 0) {
						Instantiate (enemyExplosion, this.transform.position, this.transform.rotation);
						Destroy (gameObject);
						gameController.AddScore (scoreValue);
					}
				} else {
					Destroy (gameObject);
					gameController.AddScore (scoreValue);
				}
			} else {
				Destroy (gameObject);
			}
		}
	}
}

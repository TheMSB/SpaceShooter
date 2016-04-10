using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject enemyExplosion;
	public GameObject playerExplosion;
	public int scoreValue;
	public int impactDamage;
	private GameController gameController;

	private Transform trans;

	void Start () {
		trans = GetComponent<Transform>();

		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController> ();
		} else {
			Debug.Log ("Cannot find 'GameController' script!");
		}
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.tag != "Boundary" && other.tag != "Enemy" && other.tag != "Powerup") {
			if (explosion != null) {
				Instantiate(explosion, trans.position, trans.rotation);
			}
			if (other.tag == "Player"){
				PlayerController player = other.gameObject.GetComponent <PlayerController> ();
				player.Damage(impactDamage);
				gameController.UpdateArmor();

				if (player.stats.armor == 0){
					Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
					Destroy(other.gameObject);
					gameController.GameOver();
				}
			} else {
				Destroy(other.gameObject);
			}

			if (this.tag == "Enemy") {
				EnemyController enemy = this.gameObject.GetComponent <EnemyController> ();
				PlayerController player = other.gameObject.GetComponent <PlayerController> ();
				if (enemy != null) {
					enemy.Damage (10);
					Instantiate (enemyExplosion, this.transform.position, this.transform.rotation);
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
				Destroy(gameObject);
			}
		}
	}
}

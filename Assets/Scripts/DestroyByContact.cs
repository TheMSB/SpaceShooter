using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
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
		if (other.tag != "Boundary"){
			Instantiate(explosion, trans.position, trans.rotation);

			if (other.tag == "Player"){
				PlayerController player = other.gameObject.GetComponent <PlayerController> ();
				player.Damage(30);
				gameController.UpdateArmor();
				if (player.stats.armor == 0){
					Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
					Destroy(other.gameObject);
					gameController.GameOver();
				}
			} else {
				Destroy(other.gameObject);
			}
			gameController.AddScore(scoreValue);

			Destroy(gameObject);
		}
	}
}

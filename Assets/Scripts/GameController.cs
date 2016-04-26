using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public GameObject[] bombers;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public Text scoreText;
	public Text armorText;

	public Text gameOverText;
	public GameObject restartButton;
	public GameObject menuButton;
	public GameObject player;

	private bool gameOver;
	private int score;
	private PlayerController playerController;

	public UserProfile profile;

	void Start () {
		playerController = player.GetComponent <PlayerController> ();
		gameOver = false;
		restartButton.SetActive (false);
		menuButton.SetActive (false);
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
		UpdateArmor ();
		StartCoroutine (SpawnWaves ());
		StartCoroutine (SpawnBombers ());
	}

	IEnumerator SpawnWaves () {
		yield return new WaitForSeconds (startWait);

		while (true) {
			for (int i = 0; i < hazardCount; i++) {

				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				var spawn = hazards [Random.Range (0, hazards.Length)];

				//We don't want Trident type enemies appearing too early in the game
				if (score < 1000) {
					while (spawn.name.Equals("Trident")) {
						spawn = hazards [Random.Range (0, hazards.Length)];
					}
				}
				if (playerController.stats.armor > 30) {
					while (spawn.name.Equals("Powerup")) {
						spawn = hazards [Random.Range (0, hazards.Length)];
					}
				}
			
				Instantiate (spawn, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (gameOver) {
				restartButton.SetActive (true);
				menuButton.SetActive (true);
				break;
			}
		}
	}

	IEnumerator SpawnBombers () {
		yield return new WaitForSeconds (startWait*7);
		yield return new WaitUntil (() => score > 500);
		
		while (true) {
			float lor = Mathf.Round(Random.value);
			float spawnX = -6.0f;
			if (lor == 1.0f) {
				spawnX = 6.0f;
			} else {
				spawnX = -6.0f;
			}
			for (int i = 0; i < 5; i++) {		
				Vector3 spawnPosition = new Vector3 (spawnX, spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (bombers[Random.Range(0,bombers.Length)], spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait * 1.5f);

			if (gameOver) {
				break;
			}
		}
	}


	public void AddScore (int newScoreValue) {
		score += newScoreValue;
		UpdateScore ();
	}
	void UpdateScore () {
		scoreText.text = "Score: " + score;
	}

	public void UpdateArmor () {
		armorText.text = "Armor: " + playerController.stats.armor;
	}

	public void GameOver () {
		gameOverText.text = "Game Over!";
		profile.AddScore (score);
		Debug.Log ("added the score: " + score);
		gameOver = true;
	}

	public void RestartGame () {
		Application.LoadLevel (Application.loadedLevel);
	}

	public void ReturnMenu () {
		Application.LoadLevel (0);
	}


}

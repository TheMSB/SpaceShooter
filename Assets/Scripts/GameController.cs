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
	public GameObject player;

	private bool gameOver;
	private int score;
	private PlayerController playerController;

	void Start () {
		playerController = player.GetComponent <PlayerController> ();
		gameOver = false;
		restartButton.SetActive (false);
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
				Instantiate (hazards[Random.Range(0,hazards.Length)], spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (gameOver) {
				restartButton.SetActive (true);
				break;
			}
		}
	}

	IEnumerator SpawnBombers () {
		yield return new WaitForSeconds (startWait*7);
		
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
		gameOver = true;
	}

	public void RestartGame () {
		Application.LoadLevel (Application.loadedLevel);
	}
}

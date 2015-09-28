using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public Text scoreText;
	public Text armorText;
	//public Text restartText;
	public Text gameOverText;
	public GameObject restartButton;
	public GameObject player;

	private bool gameOver;
	private bool restart;
	private int score;
	private PlayerController playerController;

	void Start () {
		playerController = player.GetComponent <PlayerController> ();
		gameOver = false;
		restart = false;
//		restartText.text = "";
		restartButton.SetActive (false);
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
		UpdateArmor ();
		StartCoroutine (SpawnWaves ());
	}

//	void Update () {
//		if (restart) {
//			if (Input.GetKeyDown (KeyCode.R)) {
//				Application.LoadLevel (Application.loadedLevel);
//			}
//		}
//	}

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
				//restartText.text = "Press 'R' for restart";
				restartButton.SetActive (true);
				restart = true;
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
		gameOver = true;
	}

	public void RestartGame () {
		Application.LoadLevel (Application.loadedLevel);
	}
}

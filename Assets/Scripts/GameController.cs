using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;	//The hazards that we can spawn each wave
	public GameObject[] bombers;	//The units we can use during a bomber run
	public Vector3 spawnValues;		//The range of positions we can use for spawning
	public int hazardCount;			//The amount of hazards we should spawn each wave
	public float spawnWait;			//Time to wait between each spawn
	public float startWait;			//Time to wait at start of game before spawning
	public float waveWait;			//Time to wait between waves

	public Text scoreText;			//Reference to the text object displaying score
	public Text armorText;			//Reference to the text object displaying armor

	public Text gameOverText;			//Reference to the game over text
	public GameObject restartButton;	//reference to the button that restarts
	public GameObject menuButton;		//reference to the button that returns to menu
	public GameObject player;			//reference to our player object

	private bool gameOver;						//Internal state reference
	private int score;							//Internal score reference
	private PlayerController playerController;	//Refernce to the playerController

	public UserProfile profile;			//Reference to the global preferences

	void Start () {
		playerController = player.GetComponent <PlayerController> ();
		//Ensure that our game state has been reset.
		gameOver = false;
		restartButton.SetActive (false);
		menuButton.SetActive (false);
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
		UpdateArmor ();

		//Start the new game.
		StartCoroutine (SpawnWaves ());
		StartCoroutine (SpawnBombers ());
	}

	/**
	 * After a specified start delay, spawn a specified number of hazards from our collection of hazards.
	 * Waits a specfied duration between each wave.
	 */
	IEnumerator SpawnWaves () {
		yield return new WaitForSeconds (startWait);

		while (true) {
			for (int i = 0; i < hazardCount; i++) {

				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				var spawn = hazards [Random.Range (0, hazards.Length)];

				//Only spawn powerups when the player is in trouble
				if (playerController.stats.armor > 30) {
					while (spawn.name.Equals("Powerup")) {
						spawn = hazards [Random.Range (0, hazards.Length)];
					}
				}

				//We don't want Trident type enemies appearing too early in the game
				if (score < 1000) {
					while (spawn.name.Equals("Trident") || spawn.name.Equals("Powerup")) {
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

	/**
	 * After a specified start delay and score, spawn a specified number of bombers from our collection of bombers.
	 * Waits a specfied duration between each wave. 
	 * Bombing enemies strafe diagonally across the screen instead of just moving down.
	 */
	IEnumerator SpawnBombers () {
		//Ensure that bombers only spawn when the player has reached a certain point in the game
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

	/**
	 * Add the specified score value to the current score.
	 */
	public void AddScore (int newScoreValue) {
		score += newScoreValue;
		UpdateScore ();
	}
	/**
	 * Updates the displayed score to the actual state.
	 */
	void UpdateScore () {
		scoreText.text = "Score: " + score;
	}
	/**
	 * Updates the displayed armor value to the actual state.
	 */
	public void UpdateArmor () {
		armorText.text = "Armor: " + playerController.stats.armor;
	}
	/**
	 * End the current game.
	 */
	public void GameOver () {
		gameOverText.text = "Game Over!";
		profile.AddScore (score);
		Debug.Log ("added the score: " + score);
		gameOver = true;
	}
	/**
	 * Restarts the current game.
	 */
	public void RestartGame () {
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}
	/**
	 * Returns to the main menu.
	 */
	public void ReturnMenu () {
		SceneManager.LoadScene (0);
	}


}

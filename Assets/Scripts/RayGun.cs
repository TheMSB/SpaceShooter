using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]

/**
 * A specialized weapon type that shoots uninterupted laser beams.
 */
public class RayGun : WeaponController 
{
	RaycastHit hit;
	public float range = 300.0f;	//Maximum laser range
	LineRenderer line;				//The line we draw along the raycast
	public Material lineMaterial;	//The material we use to draw the line

	public GameObject hitEffect;			//Impact effect reference
	public GameObject playerExplosion;		//The explosion displayed when we destroy the player
	private GameController gameController;	//Reference to our gamecontroller

	private bool firing;			//Check if we are currently firing

	void Start()
	{
		line = GetComponent<LineRenderer>();
		line.SetVertexCount(2);
		line.GetComponent<Renderer>().material = lineMaterial;
		line.SetWidth(2.8f, 2.85f);

		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController> ();
		} else {
			Debug.Log ("Cannot find 'GameController' script!");
		}
		InvokeRepeating ("Fire", startDelay, fireRate);
	}

	void Update()
	{
		//Make sure the laser moves with the ship
		line.SetPosition(0, transform.position);
		//Fire the laser down the screen
		Ray ray = new Ray(transform.position, -transform.forward);


		//Check if we hit something while firing
		if (Physics.Raycast (ray, out hit, range) && firing) {
			//If we hit something make sure the hit object blocks our laser
			line.SetPosition (1, new Vector3(transform.position.x, transform.position.y, hit.point.z - hit.normal.z));
			Instantiate(hitEffect, hit.transform.position, hit.transform.rotation);

			//Inflict 5 damage for each second that we hit the player
			if(hit.collider.tag.Equals("Player")) {
				PlayerController player = hit.collider.gameObject.GetComponent <PlayerController> ();
				player.Damage(5);
				gameController.UpdateArmor();

				if (player.stats.armor == 0){
					Instantiate(playerExplosion, hit.collider.transform.position, hit.collider.transform.rotation);
					Destroy(hit.collider.gameObject);
					gameController.GameOver();
				}
			}
		} else {
			//If we are not hitting something make sure the laser keeps going down the screen
			line.SetPosition(1, new Vector3(transform.position.x, transform.position.y, transform.position.z * -2));
		}

	}

	/**
	 * Fire our weapon
	 */
	void Fire () 
	{
		StartCoroutine (GetComponent<TridentWindup> ().WindupSequence ());
		StartCoroutine (FireLaser());
	}
		
	IEnumerator FireLaser () {
		//Start warming up our laser
		shotSpawn.GetComponent<ParticleSystem> ().Play();
		yield return new WaitForSeconds(4.0f);
		line.enabled = true;
		firing = true;
		StartCoroutine(FadeTo(0, 0.5f, 3.0f));

		//Keep shooting that laser
		sound.Play();
		yield return new WaitForSeconds(2.0f);
		shotSpawn.GetComponent<ParticleSystem> ().Stop ();
		//Start cooling down that laser
		StartCoroutine(FadeTo(0.5f, 0, 6.0f));
		line.enabled = false;
		firing = false;
	}

	/**
	 * Helper method to gradualy transition alpha values of rays.
	 */
	IEnumerator FadeTo(float aStart, float aEnd, float aTime)
	{
		Color tempLaserAlpha = line.material.GetColor("_TintColor");
		tempLaserAlpha.a = aStart;
		line.material.SetColor("_TintColor", tempLaserAlpha);

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			tempLaserAlpha.a = Mathf.Lerp(tempLaserAlpha.a,aEnd,t);
			line.material.SetColor("_TintColor", tempLaserAlpha);

			yield return null;
		}
	}
}
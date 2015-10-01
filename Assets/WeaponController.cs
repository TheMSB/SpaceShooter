using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public float startDelay;
	public AudioSource sound;

	// Start firing the gun using set interval or pattern
	void Start () {
		sound = GetComponent<AudioSource> ();
		InvokeRepeating ("Fire", startDelay, fireRate);
	}

	void Fire() {
		Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		sound.Play ();
	}
}

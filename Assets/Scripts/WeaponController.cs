using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public float startDelay;
	public AudioSource sound;

	private ParticleSystem muzzle;
	
	// Start firing the gun using set interval or pattern
	void Start () {
		sound = GetComponent<AudioSource> ();
		InvokeRepeating ("Fire", startDelay, fireRate);
		muzzle = shotSpawn.GetComponent<ParticleSystem> ();
	}

	void Fire() {
		// If we have a muzzle flash made for us then use it
		if (muzzle != null) {
			muzzle.Emit(1);
		}
		GameObject shotRef = Instantiate(shot, shotSpawn.position, shotSpawn.rotation) as GameObject;
		// Ensure that the velocity of the shooter is added to the shot
		shotRef.transform.GetComponent<Rigidbody> ().AddForce (this.gameObject.transform.GetComponent<Rigidbody>().velocity, ForceMode.VelocityChange);
		sound.Play ();

	}
}

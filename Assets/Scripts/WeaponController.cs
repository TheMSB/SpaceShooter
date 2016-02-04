using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

	public GameObject shot;
	public float shotThrust;
	private ParticleSystem muzzle;
	public Transform shotSpawn;
	public float fireRate;
	public float startDelay;
	public AudioSource sound;

	// Start firing the gun using set interval or pattern
	void Start () {
		sound = GetComponent<AudioSource> ();
		InvokeRepeating ("Fire", startDelay, fireRate);
		muzzle = shotSpawn.GetComponent<ParticleSystem> ();
	}

	void Fire() {
		if (muzzle != null) {
			muzzle.Emit(1);
		}
		GameObject shotRef = Instantiate(shot, shotSpawn.position, shotSpawn.rotation) as GameObject;
		shotRef.transform.GetComponent<Rigidbody> ().AddForce (this.gameObject.transform.GetComponent<Rigidbody>().velocity, ForceMode.VelocityChange);
		Debug.Log (this.gameObject.name);
		sound.Play ();

	}
}

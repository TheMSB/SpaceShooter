using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

[System.Serializable]
public class Stats {
	public int armor;
	public int maxArmor;
}

public class PlayerController : MonoBehaviour {

	public Rigidbody rigbody;
	public float speed;
	public float tilt;
	public Boundary boundary;
	public Stats stats;
	
	public GameObject shot;
	public Transform shotSpawn;
	public Transform shotSpawnDoubleLeft;
	public Transform shotSpawnDoubleRight;
	public float fireRate;
	public SimpleTouchPad touchPad;
	public SimpleTouchAreaButton areaButton;

	private float nextFire;
	private int powerLevel;
	private Quaternion calibrationQuaternion;
	private AudioSource sound;

	void Start () {
		sound = GetComponent<AudioSource> ();
		powerLevel = 0;
	}

	void Update () {
		if (Time.time > nextFire) {
			if (areaButton.CanFire ()) {
				nextFire = Time.time + fireRate;
				switch (powerLevel) {	
					case 1: {
						Instantiate(shot, shotSpawnDoubleLeft.position, shotSpawn.rotation);
						Instantiate(shot, shotSpawnDoubleRight.position, shotSpawn.rotation);
						break;
					}
					default: {
						Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
						break;
					}

				}
				sound.Play();
			}
		}
	}

 	void FixedUpdate () {
		// The Vector3 movement is overwritten once each update by a platform dependant code block
		Vector3 movement;
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			Vector2 direction = touchPad.GetDirection ();
			movement = new Vector3(direction.x, 0.0f, direction.y); 
		} else {
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
			
			movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		}

		rigbody.velocity = movement * speed;
		rigbody.position = new Vector3 
			(
				Mathf.Clamp (rigbody.position.x, boundary.xMin, boundary.xMax), 
				0.0f, 
				Mathf.Clamp (rigbody.position.z, boundary.zMin, boundary.zMax)
				);
		
		rigbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigbody.velocity.x * -tilt);
	}

	public void Damage (int damageAmount) {
		if (stats.armor - damageAmount < 0) {
			stats.armor = 0;
		} else {
			stats.armor = stats.armor - damageAmount;
		}
	}

	public void Heal (int healAmount) {
		if (stats.armor + healAmount <= stats.maxArmor) {
			stats.armor += healAmount;
		} else {
			stats.armor = stats.maxArmor;
		}
	}

	public void setWeapon(int i) {
		powerLevel = i;
	}
}

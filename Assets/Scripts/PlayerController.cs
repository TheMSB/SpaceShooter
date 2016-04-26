using UnityEngine;
using System.Collections;

//The boundry that constrains our movement
[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

/**
 * Implementation of the UnitController for our player instance.
 */
public class PlayerController : UnitController {

	public Rigidbody rigbody;	//Reference to the Rigidbody that controls our physics
	public float speed;			//How fast we can move
	public float tilt;			//How far we tilt
	public Boundary boundary;	//Reference our boundary field
	
	public GameObject shot;					//Reference to our weapon projectile

	//Reference to the diffirent spawn locations for shots
	public Transform shotSpawn;				
	public Transform shotSpawnDoubleLeft;
	public Transform shotSpawnDoubleRight;

	public float fireRate;						//Cooldown between consecutive shots
	public SimpleTouchPad touchPad;				//Touch controller for movement
	public SimpleTouchAreaButton areaButton;	//Touch controller for firing

	private float nextFire;						//Internal reference to current cooldown
	private int powerLevel;						//The current weapon level
	private Quaternion calibrationQuaternion;	//Internal calibration value
	private AudioSource sound;					//Reference to our audiosource

	void Start () {
		sound = GetComponent<AudioSource> ();
		powerLevel = 0;
	}

	//If our fire button is pressed and we are off cooldown then fire
	void Update () {
		if (Time.time > nextFire) {
			if (areaButton.CanFire ()) {
				nextFire = Time.time + fireRate;

			}
		}
	}

	// Constantly update the player to reflect movement commands
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

	/**
	 * Levels up the player's weapons, cannot exceed level 2.
	 */
	public void levelUp() {
		if (powerLevel < 1) {
			powerLevel++;
		}
	}

	/**
	 * Fires the current player weapon regardless of cooldowns.
	 */
	private void FireWeapon () {
		//Check which weapon we need to fire.
		switch (powerLevel) {	
		case 1:
			{
				Instantiate (shot, shotSpawnDoubleLeft.position, shotSpawn.rotation);
				Instantiate (shot, shotSpawnDoubleRight.position, shotSpawn.rotation);
				break;
			}
		default:
			{
				Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
				break;
			}

		}
		sound.Play ();
	}
}

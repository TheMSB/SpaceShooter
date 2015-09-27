using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	public Rigidbody rigidbody;
	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public SimpleTouchPad touchPad;
	public SimpleTouchAreaButton areaButton;

	private float nextFire;
	private Quaternion calibrationQuaternion;
	private AudioSource sound;

	void Start () {
		sound = GetComponent<AudioSource> ();
		CalibrateAccelerometer ();
	}

	void Update () {
		if (Time.time > nextFire) {
			if (areaButton.CanFire ()) {
			//if (areaButton.CanFire () || (Application.platform != RuntimePlatform.IPhonePlayer && Input.GetButton("Fire1"))) {
				nextFire = Time.time + fireRate;
				Instantiate(shot, shotSpawn.position, shotSpawn.rotation) ;
				sound.Play();
			}
		}
	}

 	void FixedUpdate () {
		// The Vector3 movement is overwritten once each update by a platform dependant code block
		Vector3 movement;
		//if (Application.platform == RuntimePlatform.IPhonePlayer) {
			Vector2 direction = touchPad.GetDirection ();
			movement = new Vector3(direction.x, 0.0f, direction.y); 

			//		Iphone input scheme using the accelerometer
			//		Vector3 accelerationRaw = Input.acceleration;
			//		Vector3 acceleration = FixAccelleration (accelerationRaw);
			//		Vector3 movement = new Vector3(acceleration.x, 0.0f, acceleration.y); 

//		} else {
//			float moveHorizontal = Input.GetAxis ("Horizontal");
//			float moveVertical = Input.GetAxis ("Vertical");
//			
//			movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
//		}

		rigidbody.velocity = movement * speed;
		rigidbody.position = new Vector3 
			(
				Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax), 
				0.0f, 
				Mathf.Clamp (rigidbody.position.z, boundary.zMin, boundary.zMax)
				);
		
		rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -tilt);
	}

	void CalibrateAccelerometer () {
		Vector3 accelerationSnapshot = Input.acceleration;
		Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3 (0.0f, 0.0f, -1.0f), accelerationSnapshot);
		calibrationQuaternion = Quaternion.Inverse (rotateQuaternion);
	}

	Vector3 FixAcceleration (Vector3 acceleration) {
		Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
		return fixedAcceleration;
	}

}

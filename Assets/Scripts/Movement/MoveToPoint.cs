using UnityEngine;
using System.Collections;

public class MoveToPoint : MonoBehaviour {

	/**
	 * This movement type is meant for straight lined movement like bombing runs
	 */

	public float dodge;
	public float smoothing;
	public float tilt;
	public Vector2 startWait;	// this is so we can use randomrange on the value
	public Vector2 maneuverTime;
	public Vector2 maneuverWait;
	public Boundary boundary;
	
	private float currentSpeed;
	private float targetManeuver;
	private Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		currentSpeed = rb.velocity.z;
		StartCoroutine (Strafe());
	}

	IEnumerator Strafe () {
		yield return new WaitForSeconds (Random.Range (startWait.x, startWait.y));

		targetManeuver = dodge * -Mathf.Sign (transform.position.x);
		yield return new WaitForSeconds (Random.Range(maneuverTime.x, maneuverTime.y));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float newManeuver = Mathf.MoveTowards (rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
		rb.velocity = new Vector3 (newManeuver, 0.0f, currentSpeed);
		rb.position = new Vector3 (
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
			);
		
		rb.rotation = Quaternion.Euler (0, 0, rb.velocity.x * -tilt);
	}
}

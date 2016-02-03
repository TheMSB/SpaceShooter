using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	private Rigidbody rigbody;
	public float speed;

	void Start () {
		rigbody = GetComponent<Rigidbody> ();
		rigbody.velocity = transform.forward * speed;
	}
}

using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	private Rigidbody rigidbody;
	public float speed;

	void Start () {
		rigidbody = GetComponent<Rigidbody> ();
		rigidbody.velocity = transform.forward * speed;
	}
}

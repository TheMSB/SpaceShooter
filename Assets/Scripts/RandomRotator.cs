using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour {

	public Rigidbody rigbody;
	public float tumble;

	void Start () {
		rigbody.angularVelocity = Random.insideUnitSphere * tumble;
	}
}

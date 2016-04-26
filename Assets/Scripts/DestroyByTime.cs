using UnityEngine;
using System.Collections;
/**
 * Destroys object after a specified lifetime.
 */
public class DestroyByTime : MonoBehaviour {

	public float lifetime;	//Time before object is destroyed

	void Start () {
		Destroy (gameObject, lifetime);
	}
}

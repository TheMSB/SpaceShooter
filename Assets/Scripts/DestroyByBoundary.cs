using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {
	/** Destroy any and all objects leaving the boundary.*/
	void OnTriggerExit(Collider other) {
		Destroy(other.gameObject);
	}
}

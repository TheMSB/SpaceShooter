using UnityEngine;
using System.Collections;

public class TridentWindup : MonoBehaviour {

	public Rigidbody rigbody;
	public float speed;
	public float duration;
	public AudioSource sound;

	private bool moving;

	void Start () {
		moving = false;
		rigbody.maxAngularVelocity = speed;
	}

	void Update () {
		if (!moving) {
			rigbody.transform.rotation = Quaternion.RotateTowards (rigbody.transform.rotation, Quaternion.identity, 15.0f * Time.deltaTime);
		}
	}

	public IEnumerator WindupSequence() {
		sound.Play();
		moving = true;
		while(rigbody.angularVelocity.z != speed){
			rigbody.angularVelocity = new Vector3(0,0,(rigbody.angularVelocity.z + speed*0.01f));
			yield return new WaitForSeconds(0.01f);
		}
		yield return new WaitForSeconds(duration);

		while(rigbody.angularVelocity.z >= 0){
			rigbody.angularVelocity = new Vector3(0,0,(rigbody.angularVelocity.z - speed*0.03f));
			yield return new WaitForSeconds(0.1f);
		}
		rigbody.angularVelocity = new Vector3(0,0,0);
		moving = false;
	}
}

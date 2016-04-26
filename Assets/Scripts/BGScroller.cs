using UnityEngine;
using System.Collections;

public class BGScroller : MonoBehaviour 
{
	
	public float scrollSpeed;		//The speed at which the background should scroll
	public float tileSizeZ;			//The size of the background tile on the Z-axis
	
	private Vector3 startPosition;	//The starting position of the background tile
	
	void Start () 
	{
		startPosition = transform.position;
	}

	//Ensure that the background will continuously scroll
	void Update ()
	{
		float newPosition = Mathf.Repeat (Time.time * scrollSpeed, tileSizeZ);
		transform.position = startPosition + Vector3.forward * newPosition;
	}
}
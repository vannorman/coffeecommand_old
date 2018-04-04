using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollowTarget : MonoBehaviour {

	public Transform target;
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = Vector3.Lerp (transform.position, target.position, Time.deltaTime * 3f);
		transform.rotation = Quaternion.Lerp (transform.rotation, target.rotation, Time.deltaTime * 2f);
	}
}

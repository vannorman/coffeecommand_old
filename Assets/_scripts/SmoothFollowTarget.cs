using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollowTarget : MonoBehaviour {

	public Transform target;
	public float followSpeed = 3f;
	// Update is called once per frame
	void LateUpdate () {
		transform.position = Vector3.Lerp (transform.position, target.position, Time.deltaTime * followSpeed);
		transform.rotation = Quaternion.Lerp (transform.rotation, target.rotation, Time.deltaTime * followSpeed / 1.5f);
	}
}

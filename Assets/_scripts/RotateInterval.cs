using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInterval : MonoBehaviour {

	public float intervalAverage = 5f;

	Quaternion targetRot = new Quaternion();

	float t = 0;
	// Update is called once per frame
	void Update () {
		t -= Time.deltaTime;
		if (t < 0) {
			t = Random.Range (intervalAverage / 1.5f, intervalAverage * 1.5f);
			targetRot = Quaternion.Euler (new Vector3 (0, Random.Range (0, 360), 0));
		}
		float rotSpeed = 40;
		transform.rotation = Quaternion.RotateTowards (transform.rotation, targetRot, Time.deltaTime * rotSpeed);
	}
}

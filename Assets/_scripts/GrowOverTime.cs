using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowOverTime : MonoBehaviour {

	public float growFactor = 1;
	// Update is called once per frame
	void Update () {
		transform.localScale += Vector3.one * Time.deltaTime * growFactor;
	}
}

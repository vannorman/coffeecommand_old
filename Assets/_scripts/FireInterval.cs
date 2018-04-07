using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireInterval : MonoBehaviour {

	public float intervalAverage = 5f;
	public int bulletsPerInterval = 3;
	public float smallInterval = 0.4f;
	public float fireSpeed = 300;
	public GameObject bulletPrefab;
	public Transform fireT;
	float t = 0;
	// Update is called once per frame
	void Update () {
		t -= Time.deltaTime;
		if (t < 0) {
			t = Random.Range (intervalAverage / 1.5f, intervalAverage * 1.5f);
			StartCoroutine(Fire ());
		}
	}

	IEnumerator Fire(){
		for (int i = 0; i < bulletsPerInterval; i++) {
			GameObject bullet = (GameObject)Instantiate (bulletPrefab, fireT.position, fireT.rotation);
			bullet.GetComponent<Rigidbody> ().AddForce (fireT.forward * fireSpeed);
			yield return new WaitForSeconds (smallInterval);
		}
	}
}

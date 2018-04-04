using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : MonoBehaviour {

	public List<Collider> hitObjects = new List<Collider>();
	
	// Update is called once per frame
	void Update () {
		hitObjects.Clear();
		foreach(RaycastHit hit in Physics.RaycastAll(Camera.main.ScreenPointToRay(new Vector3(Screen.width/2f,Screen.height/2f,1)))){
			Debug.Log("Phone hitting:"+hit.collider.name);
			hitObjects.Add(hit.collider);
			// 
			
		}
	}
}

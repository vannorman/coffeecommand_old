using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionDetector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit = new RaycastHit ();
		if (Physics.Raycast (Camera.main.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0f)), out hit)) {
			MetalOnion mo = hit.collider.gameObject.GetComponent<MetalOnion> ();
			if (mo) {
				mo.CameraHovering ();
				Debug.Log ("Cam hit;" + mo.name);
				DebugText.SetCamHoverObj ("cam hit:" + hit.collider.name);
			}
			
		} else {
		
			DebugText.SetCamHoverObj ("cam hit: no hit");
		}
				
	}
}

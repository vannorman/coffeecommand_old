using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CC : MonoBehaviour {

	public PlaneAnchorManager planeAnchorManagerRef;
	public static PlaneAnchorManager planeAnchorManager;
	public GameObject planePrefab;

	// Use this for initialization
	void Start () {
		planeAnchorManager = planeAnchorManagerRef;
		UnityEngine.XR.iOS.UnityARUtility.InitializePlanePrefab (planePrefab);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

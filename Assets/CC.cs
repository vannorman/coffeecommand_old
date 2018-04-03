using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CC : MonoBehaviour {

	public PlaneAnchorManager planeAnchorManagerRef;
	public static PlaneAnchorManager planeAnchorManager;

	// Use this for initialization
	void Start () {
		planeAnchorManager = planeAnchorManagerRef;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

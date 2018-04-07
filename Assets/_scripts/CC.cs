using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CC : MonoBehaviour {

	public PlaneAnchorManager planeAnchorManagerRef;
	public static PlaneAnchorManager planeAnchorManager;
	public GameObject planePrefab;
	public static DebugText debugText;
	public DebugText debugTextRef;

	public static ShipWeapons shipWeapons;
	public ShipWeapons shipWeaponsRef;
	// Use this for initialization
	void Start () {
		planeAnchorManager = planeAnchorManagerRef;
		debugText = debugTextRef;
		shipWeapons = shipWeaponsRef;
		UnityEngine.XR.iOS.UnityARUtility.InitializePlanePrefab (planePrefab);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

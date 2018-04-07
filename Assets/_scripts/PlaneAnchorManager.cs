using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneAnchorManager : MonoBehaviour {


	public  UnityEngine.XR.iOS.UnityARAnchorManager anchorManager;
	void Start(){
		anchorManager = new UnityEngine.XR.iOS.UnityARAnchorManager ();
	}
//	List<GameObject> shownPlanes = new List<GameObject>();
//	public void ShowPlanes(){
//		HidePlanes ();
//		foreach (UnityEngine.XR.iOS.ARPlaneAnchorGameObject plane in anchorManager.GetCurrentPlaneAnchors()) {
//			GameObject p = GameObject.CreatePrimitive (PrimitiveType.Cube);
//			p.transform.position = plane.gameObject.transform.position;
//			p.transform.localScale = plane.gameObject.transform.localScale * 0.5f;// plane.planeAnchor.transform.lossyScale;
//			Vector3 s = p.transform.localScale;
//			p.transform.localScale = new Vector3 (s.x, 0.1f, s.z);
//			p.GetComponent<Renderer> ().material.color = Color.cyan;
//			shownPlanes.Add (p);
//		}
//
//	}
//
//	public void HidePlanes(){
//		foreach (GameObject o in shownPlanes) {
//			Destroy (o);
//		}
//		shownPlanes.Clear ();
//	}
//
	float lastNearestPlaneTime = 0f;
	float nearestPlaneSeekInterval = 2;
	GameObject cachedPlane = null;
	public GameObject GetNearestPlane(Transform nearObj, float r){
		string d = "Seeking planes: ";

		if (Mathf.Abs (lastNearestPlaneTime - Time.time) > nearestPlaneSeekInterval) {
//			UnityEngine.XR.iOS.UnityARMatrixOps.GetPosition
			GameObject nearest = null;
			lastNearestPlaneTime = Time.time;
			float nearDist = Mathf.Infinity;
			int i = 0;
			foreach (PlaneInfo pi in FindObjectsOfType<PlaneInfo>()) {
				float curDist = Vector3.Magnitude (pi.gameObject.transform.position - nearObj.position);
				d += i + ": " + curDist+",";
				if (curDist < nearDist && curDist < r) {
					curDist = nearDist;
					nearest = pi.gameObject;
					d += "nearest!..";

				}
				i++;
			}
			cachedPlane = nearest;
			DebugText.SeekPlanes(d);
			return nearest;
		} else {
			DebugText.SeekPlanes(d + "cached plane");
			return cachedPlane;
		}
	}
}

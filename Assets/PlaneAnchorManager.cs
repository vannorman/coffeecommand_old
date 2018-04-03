using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneAnchorManager : MonoBehaviour {


	public  UnityEngine.XR.iOS.UnityARAnchorManager anchorManager;
	void Start(){
		anchorManager = new UnityEngine.XR.iOS.UnityARAnchorManager ();
	}
	List<GameObject> shownPlanes = new List<GameObject>();
	public void ShowPlanes(){
		HidePlanes ();
		foreach (UnityEngine.XR.iOS.ARPlaneAnchorGameObject plane in anchorManager.GetCurrentPlaneAnchors()) {
			GameObject p = GameObject.CreatePrimitive (PrimitiveType.Cube);
			p.transform.position = plane.gameObject.transform.position;
			p.transform.localScale = plane.gameObject.transform.localScale;// plane.planeAnchor.transform.lossyScale;
			p.GetComponent<Renderer> ().material.color = Color.cyan;
			shownPlanes.Add (p);
		}

	}

	public void HidePlanes(){
		foreach (GameObject o in shownPlanes) {
			Destroy (o);
		}
		shownPlanes.Clear ();
	}

	float lastNearestPlaneTime = 0f;
	float nearestPlaneSeekInterval = 2;
	GameObject cachedPlane = null;
	public GameObject GetNearestPlane(Transform nearObj){
		if (Mathf.Abs (lastNearestPlaneTime - Time.time) > nearestPlaneSeekInterval) {
			GameObject nearest = null;
			lastNearestPlaneTime = Time.time;
			float nearDist = Mathf.Infinity;
			foreach (UnityEngine.XR.iOS.ARPlaneAnchorGameObject plane in anchorManager.GetCurrentPlaneAnchors()) {
				float curDist = Vector3.Magnitude (plane.gameObject.transform.position - nearObj.position);
				if (curDist < nearDist) {
					curDist = nearDist;
					nearest = plane.gameObject;
				}
			}
			cachedPlane = nearest;
			return nearest;
		} else {
			return cachedPlane;
		}
	}
}

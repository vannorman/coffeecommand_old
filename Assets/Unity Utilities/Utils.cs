using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Utils2 {

	public static Quaternion FlattenRotation(Quaternion rot){
		rot.eulerAngles = new Vector3(0,rot.eulerAngles.y,0);
		return rot;
	}
		
	public static Vector3 FlattenVector(Vector3 vectorA){
		return new Vector3(vectorA.x,0,vectorA.z);
	}

	public static bool PointVisibleToCamera(Vector3 p, Camera c){
		Vector3 cp = c.WorldToViewportPoint (p);
		return 
			cp.x >= 0 &&
			cp.x <= 1 &&
			cp.y >= 0 &&
			cp.y <= 1 &&
			cp.z > 0;
	}

	public static float HighestY(Transform tt){
		// Takes the transformand all children and goes through each inddividual mesh's world bounds to figure out what is the highest worldspace point occupied by any child
		// Useful if you want to know what the highest position of a group of objects is in world space, e.g. "does this group of objects cross some surface of height Y"
		float highestY = Mathf.NegativeInfinity;
		foreach(Transform t in tt){
			foreach(MeshRenderer r in t.GetComponentsInChildren<MeshRenderer>()){
				float newY = r.gameObject.transform.position.y + r.bounds.extents.y;
				if (newY > highestY) {
					highestY = newY;
				}
			}
		}
		return highestY;
	}



	public static float LowestY(Transform tt){
		// same sas highesty but for lowest point
		// Used together, you can get the total world space height of a group of objects
		float lowestY = Mathf.Infinity;
		foreach(Transform t in tt){
			foreach(MeshRenderer r in t.GetComponentsInChildren<MeshRenderer>()){
				float newY = r.gameObject.transform.position.y - r.bounds.extents.y;
				if (newY < lowestY) {
					lowestY = newY;
				}
			}
		}
		return lowestY;
	}

	public static float RealHeight(Transform t){
		return HighestY(t) - LowestY(t);
	}

	public static Bounds boundsOfChildren(Transform t){
		// What is the total encapsulating bounds of all children of t?
		Bounds b = new Bounds(Vector3.zero,Vector3.zero);
		foreach(MeshRenderer r in t.GetComponentsInChildren<MeshRenderer>()){
			if (b.size == Vector3.zero){
				b = r.bounds;	
			} else {
				b.Encapsulate(r.bounds);
			}
		}
		return b;
	}
	

	public static float CubicInOut(float x){ // Note - not actually cubic, need to change the name
		// a bell curve style curve beginning and ending at zero.
		// used for elevator to start and end slow but move quite fast in the middle.
		float ret = new AnimationCurve(
			new Keyframe(0, 0), 
			new Keyframe(0.02f, 0.05f), 
			new Keyframe(0.2f, 0.75f), 
			new Keyframe(0.3f, 0.9f), 
			new Keyframe(0.4f, 1), 
			new Keyframe(0.6f, 1), 
			new Keyframe(0.7f, 0.9f), 
			new Keyframe(0.8f, 0.75f),
			new Keyframe(0.98f, 0.05f),
			new Keyframe(1, 0)
		).Evaluate(x);
//		Debug.Log("ret:"+ret);
		return ret;
	}

	public static float BellCurve(float x){
		// approximates a bell curve of max y value 1 between interval 0, 1 on x axis.
		float ret = (1+Mathf.Cos(Mathf.PI * 2 * x - Mathf.PI)) / 2f;
		return ret;
	}



	public static Vector3[] GetCircleOfPoints(float degreesToComplete=360, float radius=150, float scale=5){ // lower scale = higher point count
		// This method can be used to get a set of Vector3 points that draw a cirle about the Y axis.
		// Useful if you want to cast a spell that creates an arrangement of objects in a circle around the spellcaster or target.
		// Note: The Vector3[] array will exist in local space

		int count = (int)(degreesToComplete * radius / scale / 60);
		float arcLength = degreesToComplete / count;
		Vector3[] ret = new Vector3[count];
		for (int i=0;i<count;i++){
			// commented Debug.Log ("radius:"+radius);
			float xPos = Mathf.Sin(Mathf.Deg2Rad*i*arcLength)*(radius); // x and y calculated with "trigonometry"
			float yPos = Mathf.Cos(Mathf.Deg2Rad*i*arcLength)*(radius);
			ret[i] = new Vector3(xPos,0,yPos);
		}
		return ret;
	}

	public static bool IntervalElapsed(float t){
		// A handy replacement for using "timers" in various scripts to track actions over intervals
		/*
		 * Usage: 
		 * if (Utils.IntervalElapsed(2)) {
		 * 		// This action occurrs every 2 seconds.
		 * }
		*/
		return Time.realtimeSinceStartup > t && Mathf.Abs(((Time.realtimeSinceStartup - t) % t)-t)<Time.unscaledDeltaTime;
	}

	

	public static Transform  GetClosestObjectOfType<T>(Transform origin) where T : MonoBehaviour {
		// Often not the most efficient, but will help you find the closest object which has type <T> to a transform, "origin"
		/*
		 * Usage: 
		 * Enemy e = Utils.GetClosestObjectOfType<Enemy>(Player.transform);
		 * // if any Enemy objects exist in the scene, e is now a reference to the closest Enemy to the Player
		 * */

		Transform closest = null;
		foreach(Component oo in Component.FindObjectsOfType(typeof(T))){
			GameObject o =  oo.gameObject;
			if (!closest){
				closest = o.transform;
			} else if (Vector3.SqrMagnitude(origin.position-o.transform.position) < Vector3.SqrMagnitude(closest.position-origin.position)){
				closest = o.transform;
			}
		}
		return closest;
	}

}

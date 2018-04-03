using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetalOnion : MonoBehaviour {

	// appears randomly spread out throughout the scene on start
	// float around until player gets "near" to one, then it floats to the nearest "surface" until brown  dots > 50
	// now ready to be "unrwapped"
	// player grabs one of the tendrils "grab" ui
	// player moves phone in correct direction dragging the UI radial fill around, which starts as gray and slowly fades to green as the sweep is completed.
	// hatpics doot doot as you drag
	// After a sweep is completed, some success sounds, particles, another tendril pops out, maybe 3 total will pop
	// after 3rd tendril drag is completed, the onion plants!

	public enum State {
		Floating,
		Ready,
		Unwrapping,
		Unwrapped
	}

	public State state;


	public Image unwrapIndicator;
	float targetFillAmount = 0;


//	public Tendril tendril; // the initial tendril is "deactivated" and will serve as a prefab for additional tendrils
	List<Tendril> tendrils = new List<Tendril>();


	void Start(){
		unwrapIndicator.fillAmount = 0;
		DebugText.SetOnionCount(FindObjectsOfType<MetalOnion>().Length.ToString());
		tendrils.AddRange(GetComponentsInChildren<Tendril> ());
	}

	public void SetState(State newState){
		state = newState;
		DebugText.SetOnionState (state.ToString());

	}

	float cameraHoverTimer = 0f;
	float cameraHoverThreshhold  = 1.5f; // after this passed, tendril will lock out and allow rotation.
	bool cameraHovering = true; // for initializeation of camhovercountdown in update if camhovering false
	public void CameraHovering(){
//		Debug.Log ("camhov:" + cameraHoverTimer);
		cameraHoverTimer = 0.2f;
	}


	float camHoverCountdown = 0;
	Quaternion targetRot;
	// Update is called once per frame
	void Update () {

		#if UNITY_EDITOR
		if (Input.GetKey (KeyCode.L)) {
			CameraHovering ();
		}
		#endif

		cameraHoverTimer -= Time.deltaTime;
		if (cameraHoverTimer > 0) {
			cameraHovering = true; 
		}

//		if (cameraHovering) DebugText.SetCamHoverObj ("onion:" + this.name + " at:" + Time.time);


		switch (state) {
		case State.Floating:
			if (cameraHovering) {
				TryMoveToNearestPlane ();
			}
			if (CloseToAPlane ()) {
				SetState (State.Ready);
			}

			// need to plant to a plane before continuing.
			break;
		case State.Ready:
			if (cameraHovering) {
				camHoverCountdown -= Time.deltaTime;
				//				Debug.Log ("camhover count:" + camHoverCountdown);
				if (camHoverCountdown < 0) {
					SetState (State.Unwrapping);
				}


			}
			// we are at a plane and ready for unwrap
			break;
		case State.Unwrapping:
			if (cameraHovering) {
				
				targetRot = Quaternion.LookRotation (Camera.main.transform.position - transform.position);


			}
			break;
		case State.Unwrapped:
			break;
		default:
			break;

		}

		if (state != State.Unwrapped) {
			

				
			
			if (cameraHovering) {
				// We're looking at this onion. First move it near to a plane


			} else {
				foreach (Tendril t in tendrils) {
					//				Debug.Log ("popin");
					t.PopIn (); //SetState (Tendril.State.Out);
				}
			}
			float rotSpeed = 2f;
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRot, Time.deltaTime * rotSpeed);

			float fillSpeed = 1f;
			unwrapIndicator.fillAmount = Mathf.MoveTowards (unwrapIndicator.fillAmount, targetFillAmount, Time.deltaTime * fillSpeed);

			if (state == State.Unwrapping) {
				targetFillAmount = 1;
				if (unwrapIndicator.fillAmount > 0.99) {
					unwrapIndicator.fillAmount = 1;
					SetState (State.Unwrapped);
					GetComponent<AudioSource> ().Play ();
				}
			}
		}
	}

	void StoppedUnwrapping(){
		// user failed to complete an unwrap
		cameraHovering = false;
		targetFillAmount = 0;
		camHoverCountdown = cameraHoverThreshhold;
		targetRot = Quaternion.Euler (Random.onUnitSphere);
		SetState (State.Ready);

	}

	GameObject 	nearest;
	bool CloseToAPlane(){
		if (nearest == null) {
			DebugText.CloseToPlane ("no nearest plane");
			return false;
		}
		float d = (transform.position - nearest.transform.position).magnitude;
		DebugText.CloseToPlane ("dist:" + d);
		return d < 1;
	}
	void TryMoveToNearestPlane () {
		
		nearest = CC.planeAnchorManager.GetNearestPlane(transform);
		if (nearest) {
			DebugText.SetPlaneInfo ("moving towards nearest..");
			float floatToPlanSpeed = 1f;
			transform.position = Vector3.MoveTowards (transform.position, nearest.transform.position, Time.deltaTime * floatToPlanSpeed);

		}	
	}

}

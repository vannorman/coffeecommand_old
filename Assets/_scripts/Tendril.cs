using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tendril : MonoBehaviour {

	// On awake, it should "pop out" immediately
	// once out it waits for the user to "grab" it, maybe a gaze-based circle timer  over the reticle that locks onto the tendril after 1.2 seconds
	// When player moves the phone, the tendril tries to rotate along with the phone


	public Transform stalk; // positioned halfway between the bulb and the center and scaled appropriately.
	public void UpdateStalk (){
		stalk.position = StalkPosition;
		stalk.localScale = StalkScale;
	}
	public Vector3 StalkPosition {
		get {
			return (this.transform.position + this.bulb.position)/2f;
		}
	}
	public Vector3 StalkScale {
		get {
			return new Vector3(0.05f, (this.transform.position - this.bulb.position).magnitude/2f,0.05f); 
		}
	}
	public Transform bulb;
	public Image uiProgress;
//	public Transform inT; // child transforms to "pop" the stalk from in to out when this tendril is active
	public Transform outT;
//	public Transform target;

	public Vector3 GetMaxExtendedPosition {
		get {
			return outT.position + (outT.position - transform.position).normalized * .04f; // just PAST the target position to allow for a "pop" effect
		}
	}
	public Vector3 GetMaxExtendedTarget {
		get {
			return outT.position + (outT.position - transform.position).normalized * .08f; // just PAST the target position to allow for a "pop" effect
		}
	}



	public enum State {
		Ready,
		PoppingOut, // extend to max
		PoppingOutSettling,  //  retract just a hair to settle in
		Out, // no longer moving
		PoppingIn // retracting
	}

	public State state;


	public void SetState(State newState){
		state = newState;

		switch (state) {
		case State.Ready:
			
			bulb.position = transform.position; // center
			UpdateStalk();
			break;
		case State.PoppingOut:
			GetComponent<AudioSource>().pitch = 1;
			GetComponent<AudioSource>().clip = extending;
			GetComponent<AudioSource>().Play();
			break;
		case State.PoppingOutSettling:
			GetComponent<AudioSource>().pitch = 1;
			GetComponent<AudioSource>().clip = finished;
			GetComponent<AudioSource>().Play();
			break;
		case State.PoppingIn:
			GetComponent<AudioSource>().pitch = .9f;
			GetComponent<AudioSource>().clip = extending;
			GetComponent<AudioSource>().Play();
			break;

			default:
			break;
		}

	}

	void Start() {
		
		SetState(state);
	}

	public AudioClip extending;
	public AudioClip finished;

	public void PopOut(){
		if (state == State.Ready) {
			SetState (State.PoppingOut);
		}
	}

	public void PopIn(){
		if (state == State.Out) {
			SetState (State.PoppingIn);
		}
	}

	float popSpeed = 3.8f;
	void Update(){
		

		if (Input.GetKeyDown(KeyCode.P)){
			SetState(State.PoppingOut);
//			GetComponent<AudioSource>().Play();
		} else if (Input.GetKeyDown(KeyCode.O)){
			SetState(State.PoppingIn);
//			GetComponent<AudioSource>().Play();
		}

		if (Input.GetKeyDown(KeyCode.Alpha1)){
			popSpeed += 0.2f;
			Debug.Log("<color=red>Pop:</color>"+popSpeed);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)){
			popSpeed -= 0.2f;
			Debug.Log("<color=blue>Pop:</color>"+popSpeed);
		}
		float d = 1;
		switch(state){
		case State.PoppingOut:
			bulb.transform.position = Vector3.Lerp(bulb.transform.position,GetMaxExtendedTarget,Time.deltaTime * popSpeed);

			d = (bulb.transform.position - transform.position).magnitude;
			float d1 = (GetMaxExtendedPosition - transform.position).magnitude;
			if (d > d1){
				SetState(State.PoppingOutSettling);
			}
			UpdateStalk();
			break;
		case State.PoppingOutSettling:
			bulb.transform.position = Vector3.Lerp(bulb.transform.position,outT.position,Time.deltaTime * popSpeed);
			d = (bulb.transform.position - outT.position).magnitude;
			if (d < .05f){
				SetState(State.Out);
			}

			UpdateStalk();
			break;
		case State.PoppingIn:
			popSpeed = 3.5f;
			bulb.transform.position = Vector3.Lerp(bulb.transform.position,transform.position,Time.deltaTime * popSpeed);
			d = (bulb.transform.position - transform.position).magnitude;
			if (d < .05f){
				GetComponent<AudioSource>().pitch = 1;
				GetComponent<AudioSource>().clip = finished;
				GetComponent<AudioSource>().Play();
				SetState(State.Ready);
			}
			UpdateStalk();
			break;
		case State.Out:
			// Wait for user to drag?

			break;
		default:break;

		}



	}
}

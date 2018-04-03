using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneUI : MonoBehaviour {

	public Transform target;
	public Image targetCircle;
	public Image progress;
	public AudioClip pop;
	public AudioClip targetLocked;
	AudioSource aud;
	void Start(){
		aud = GetComponent<AudioSource>();
	}


	public void PopTarget(){
		aud.clip = pop;
		aud.pitch = 1;
		aud.Play();
	}

	public void UnPopTarget(){
		aud.clip = pop;
		aud.pitch = -1;
		aud.Play();

	}

	public void SetTarget(Transform t){
		target = t;
	}
	public enum State {
		Ready,
		TargetAcquired,
		TargetLocked,
		TargetLost
	}
	public State state;

	public void SetState(State newState){
		state = newState;
		progress.fillAmount = 0;
		switch(state){
		case State.Ready:
			break;
			case State.TargetAcquired:
			PopTarget();
			break;
		case State.TargetLocked:
			aud.clip = targetLocked;
			aud.Play();
			break;
			case State.TargetLost:
			UnPopTarget();
			break;
		default:break;


		}
	}

	float fillDuration = 1.5f;
	void Update(){

		if (Input.GetKeyDown(KeyCode.T)){
			SetState(State.TargetAcquired);
		}
		switch(state){
		case State.Ready:
			break;
		case State.TargetAcquired:
			progress.fillAmount += Time.deltaTime / fillDuration;
			if (progress.fillAmount >= 1){
				SetState(State.TargetLocked);
			}
			break;
		case State.TargetLost:
			
			break;
		default:break;


		}
	}
}

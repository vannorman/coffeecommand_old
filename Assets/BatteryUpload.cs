using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryUpload : MonoBehaviour {


	void Start(){
		uploadButton.SetActive (false);
	}

	public GameObject uploadButton;
	public void SetFillAmount(float f){
		GetComponentInChildren<UnityEngine.UI.Image> ().fillAmount = f;
		if (f >= 1) {
			uploadButton.SetActive (true);	
		}
	}
}

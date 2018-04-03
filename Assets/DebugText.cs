using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugText : MonoBehaviour {

	public Text t;
	public static DebugText inst;
	void Start(){
		t = GetComponent<Text> ();
		inst = this;
//		Debug.Log ("inst set.");
	}



	string brownDots = "";
	string greenDots = "";
	string camHoverObj = "";
	string onionCount = "";

	string planesCount = "";
	string planeInfo = "";
	string onionState=  "";
	string closeToPlane = "";
	void Update () {
		
		t.text = ""
			+ brownDots + "\n"
			+ greenDots + "\n"
			+ "Cam hov obj:"+camHoverObj + "\n"
			+ "onion count:"+onionCount + "\n"
			+ "Onion State:"+onionState + "\n"
			+ "Plane info:"+planeInfo + "\n"
		+"Plane count:"+planesCount + "\n"
		+"close to plane:"+closeToPlane + "\n";
		
		
	}

		
	public static void SetBrownDots(string s){
		inst.brownDots = s;

	}

	public static void SetGreenDots(string s){
		inst.greenDots = s;
	}

	public static void SetCamHoverObj(string s){
		inst.camHoverObj = s;
	}

	public static void SetOnionCount(string s){
		inst.onionCount = s;
	}

	public static void SetPlanes(int a){
		Debug.Log ("A:" + a);
		inst.planesCount = a.ToString ();
	}

	public static void SetPlaneInfo(string s){
		inst.planeInfo = s;
	}

	public static void SetOnionState(string s){
		inst.onionState = s;
	}

	public static void CloseToPlane(string s){
		inst.closeToPlane = s;
	}
}

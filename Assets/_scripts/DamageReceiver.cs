using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour {


	public int hitPoints = 50;
	public GameObject objToDie;
	public string messageToSend;
	public void TakeDamage(int d){
		hitPoints -= d;
		if (hitPoints < 1) {
			Die ();
		}
	}

	void Die() {
//		Destroy (this.gameObject);
		objToDie.SendMessage(messageToSend);
	}

}

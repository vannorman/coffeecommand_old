using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageGiver : MonoBehaviour {

	public int damageAmount = 1;
	void OnCollisionEnter(Collision hit){
		DamageReceiver dr = hit.collider.GetComponent<DamageReceiver> ();
		if (dr)
			GiveDamage (dr);
	}

	void OnTriggerEnter(Collider collider){
		DamageReceiver dr = collider.GetComponent<DamageReceiver> ();
		if (dr)
			GiveDamage (dr);
	}

	void GiveDamage(DamageReceiver dr){
		dr.TakeDamage(damageAmount);
	}
}

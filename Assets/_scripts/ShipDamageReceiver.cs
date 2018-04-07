using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipDamageReceiver : MonoBehaviour {

	public float radius = 0.35f;
	public Renderer damageFx;
	public Image buttonDisabled;
	void Start(){
		
	}

	float damageTimeout = 2;
	float damageDuration = 2;
	void TakeDamage(){
		buttonDisabled.gameObject.SetActive (true);
		CC.shipWeapons.weaponsOnline = false;
		CC.shipWeapons.FireButtonUp ();
		damageTimeout = damageDuration;
		Color c = damageFx.material.color;

		damageFx.material.color = new Color (c.r, c.g, c.b, 1);
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Collider c in Physics.OverlapSphere(transform.position,radius)) {
			if (c.GetComponent<OnionDamagGiver> ()) {
				TakeDamage ();
			}
		}

		if (damageTimeout > 0) {
			damageTimeout -= Time.deltaTime;
			Color c = damageFx.material.color;
			float fadeSpeed = 1;
			damageFx.material.color = Color.Lerp (c, new Color (c.r, c.g, c.b, 0), Time.deltaTime * fadeSpeed);
			if (damageTimeout < 0) {
				DamageOver ();
				damageFx.material.color = new Color (c.r, c.g, c.b, 0);
			}
		}
	}

	void DamageOver () {
		buttonDisabled.gameObject.SetActive (false);
		CC.shipWeapons.weaponsOnline = true;
	}
}

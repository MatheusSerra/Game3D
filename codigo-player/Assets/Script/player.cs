using System.Collections;
using System;
using UnityEngine;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;

	public class player : MonoBehaviour {
	public List<BaseWeapon> weapons;
	public Vector2 recoilShot;


	void Start (){
	
	}		
			
	void Update (){
				InputPlayer ();
			}

		public void InputPlayer () {
				if ((Input.GetButton ("Fire1") && weapons[0].isAutomatic) || Input.GetButtonDown ("Fire1")) {
				recoilShot = weapons [0].Fire ();
				GetComponent<FirstPersonController> ().ApplyRotationOnCamera (recoilShot);	
			if (recoilShot != Vector2.zero) {
				UaiController.instace.aimUI.applySize (weapons [0].GetCurrentRecoilFactor ());
				}
			recoilShot = Vector2.zero;	
			}
			if(Input.GetKeyDown(KeyCode.R)){
					weapons[0].Reload();
			}
			if (Input.GetButton ("Fire2")) {
					weapons [0].EnableZoom ();
			} else {
					weapons [0].CancelZoom ();
				}
		} 



} //fim codigo 

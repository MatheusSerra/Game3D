using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunBehaviour : BaseWeapon {
	public int amountProjectils;
	public float spreadFactor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected override void OnFire (float damage, float range, Vector3 starPosition)
	{
		for (int i = 0; i < amountProjectils; i++) {
			Vector3 positionToSpawn = positionSpawnProjectil.position;
			positionToSpawn += new Vector3 (GetSpreadPosition (), GetSpreadPosition (), 0);

			Instantiate (bullet.gameObject, positionToSpawn, transform.rotation);
		}
	}

	private float GetSpreadPosition(){
		return Random.Range (-spreadFactor, spreadFactor);
	}

	protected override void OnReload ()
	{
		throw new System.NotImplementedException ();
	}
}

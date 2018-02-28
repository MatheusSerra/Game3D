using System.Collections;
using UnityEngine;

public class PistolBehaviour : BaseWeapon {

	// Use this for initialization
	new void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
	}

	protected override void OnFire (float damage, float range, Vector3 starPosition)
	{
		Ray rayShot = new Ray (starPosition, transform.forward);
		RaycastHit hitInfo;
		float offset = 0.05f;

		if (Physics.Raycast (rayShot, out hitInfo, range)) {
			Quaternion hitRotation = Quaternion.LookRotation (-hitInfo.normal);
			Instantiate (decalShotPrefab, hitInfo.point + (hitInfo.normal * offset), hitRotation);
		}
		Instantiate (bullet.gameObject,positionSpawnProjectil.position, transform.rotation);
	
	}

	protected override void OnReload ()
	{
		
	}
}

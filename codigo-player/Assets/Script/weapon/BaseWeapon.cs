using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseWeapon : MonoBehaviour {

	public float damage;			//dano causado pela arma
	public float range;
	public Vector2 minRecoil;		//recoil da arma
	public Vector2 maxRecoil; 
	public float recoilFactorIncrease;
	public float maxRecoilFactor;
	public float fireRateTime;			//tanto que pode disparar
	public int capacityAmmo;		//capacidade de munição 
	public int capacityAmmoOutWeapon;	//quantidade de munição para carregar fora da bala
	public Transform positionSpawnProjectil; 
	public ParticleSystem muzzleParticle;
	public float aimZoom;
	public bool isAutomatic;			//arma e automatica ou nao


	private int currentAmoutAmmo;
	private int currentAmoutAmmoOutWeapon;
	private float currentFireRateTime;
	private bool canFire = true;
	private float startZoom;
	private ParticleSystem currentMuzzle;
	private float currentRecoilFactor;
	private bool callInternalReload;

	public BulletBehaviour bullet; 
	public GameObject decalShotPrefab;

	//animations
	private Animator animator;


	// Use this for initialization
	protected void Start () {
		currentAmoutAmmo = capacityAmmo;
		currentAmoutAmmoOutWeapon = capacityAmmoOutWeapon;
		startZoom = Camera.main.fieldOfView;
		GameObject tempMuzzle = Instantiate (muzzleParticle.gameObject, positionSpawnProjectil.position, transform.rotation) as GameObject;
		currentMuzzle = tempMuzzle.GetComponent<ParticleSystem>();
		currentMuzzle.transform.SetParent (positionSpawnProjectil);
		animator = GetComponentInChildren<Animator> ();
	}
	
	// Update is called once per frame
	protected void Update () {
		currentFireRateTime += Time.deltaTime;
		if (currentFireRateTime > fireRateTime && !IsReloading()) {
			canFire = true;
		} else {
			canFire = false;
		}
		if (IsReloading()) {
			callInternalReload = false;
		}

		if(currentRecoilFactor > 0){
			currentRecoilFactor -= recoilFactorIncrease*0.3f;
		}
	}

	public Vector2 Fire(){
		if (currentAmoutAmmo > 0 && canFire && !callInternalReload) {
			Vector3 startPosition = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width / 2, Screen.height / 2, 0));
			OnFire (damage, range, startPosition);
			currentAmoutAmmo--;
			currentFireRateTime = 0;
			currentMuzzle.Play ();

			if (animator != null) {
				animator.SetTrigger ("onShot");
			}

			return GetRecoil ();
		} 
		else if (currentAmoutAmmo == 0) {
			Reload ();
			callInternalReload = true;
		}
		return Vector2.zero;
	}


	public void Reload() {
		int needAmmo = capacityAmmo - currentAmoutAmmo;
		if(IsReloading() || needAmmo == 0){
			return;
		}

		OnReload ();
		if (currentAmoutAmmoOutWeapon > needAmmo) {
			currentAmoutAmmoOutWeapon -= needAmmo;
			currentAmoutAmmo += needAmmo;
		} else {
			currentAmoutAmmo += currentAmoutAmmoOutWeapon;
			currentAmoutAmmoOutWeapon = 0;
		}
		if (animator != null){
			animator.SetTrigger ("onReload");
		}
	} // fim codigo reload

	private bool IsReloading(){
		return animator.GetCurrentAnimatorStateInfo(0).IsName("reload");
	}

	public void EnableZoom(){
		Camera.main.fieldOfView = aimZoom;
	}

	public void CancelZoom() {
		Camera.main.fieldOfView = startZoom;
	}

	protected abstract void OnFire (float damage, float range, Vector3 starPosition);
	protected abstract void OnReload ();

	private Vector2 GetRecoil(){
		Vector2 newRecoil = new Vector2 (Random.Range(minRecoil.x, maxRecoil.x),Random.Range(minRecoil.y, maxRecoil.y));
		if (currentRecoilFactor < maxRecoilFactor) {
			currentRecoilFactor += recoilFactorIncrease;
		}
		return newRecoil * currentRecoilFactor;
	}

	public float GetCurrentRecoilFactor(){
		return currentRecoilFactor;
	}
		
}

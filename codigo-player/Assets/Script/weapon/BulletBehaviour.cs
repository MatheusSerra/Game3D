using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {

	public float speed;
	public float timeToLive;


	void Start () {
		Destroy (gameObject, timeToLive);
	}


	void Update () {
		transform.Translate (Vector3.forward * speed * Time.deltaTime);
	}
	public void Setup (Vector3 position){
		transform.LookAt (position);
	}
}

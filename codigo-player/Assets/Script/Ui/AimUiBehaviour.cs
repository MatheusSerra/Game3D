using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AimUiBehaviour : MonoBehaviour {

	private Vector3 starSize;
	public float speedToReturnSize;
	public float sizeIncreaseFactor;

	// Use this for initialization
	void Start () {
		starSize = transform.localScale;	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localScale != starSize) {
			transform.localScale = Vector3.Lerp (transform.localScale, starSize, speedToReturnSize * Time.deltaTime);
		}
	}

	public void applySize(float size){
		transform.localScale += (new Vector3 (transform.localScale.x + size, transform.localScale.y + size, 0)) * sizeIncreaseFactor;
	}
}


















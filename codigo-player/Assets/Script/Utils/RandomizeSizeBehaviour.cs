using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeSizeBehaviour : MonoBehaviour {

	public float offsetSize;

	// Use this for initialization
	void Start () {
		transform.localScale *= Random.Range(1, offsetSize);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

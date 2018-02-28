using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTimeBEhavious : MonoBehaviour {

	public float timeAfterDestroy;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, timeAfterDestroy);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

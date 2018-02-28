using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class arma : MonoBehaviour {

	public GameObject bala;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Atirar(){
		Instantiate (bala, transform.position, transform.rotation);
	}
}

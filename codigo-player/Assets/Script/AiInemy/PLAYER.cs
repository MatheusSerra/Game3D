using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PLAYER : MonoBehaviour {
	/*
	public static float VIDA = 100;
	public string cena;

	void Start () {

	}

	void Update () {
		if (VIDA <= 0) {
			VIDA = 0;
			Death ();
		}
	}

	void Death (){
		SceneManager.LoadScene (cena);
	} */

	//valores testes

	public float VIDA = 100;
	public string cena;
	void Awake () {
		transform.tag = "Player"; 
	}
	void Start(){
	}

	void Update () {
		if (VIDA <= 0) {
			VIDA = 0;
			Death ();
		}	
	}
	void Death () {
		SceneManager.LoadScene (cena);		
	} 

}

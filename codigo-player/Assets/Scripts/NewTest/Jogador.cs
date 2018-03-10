using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jogador : MonoBehaviour {

		public int VIDA = 100;
		public string cena;

		void Awake () {
			transform.tag = "Player"; 
			
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
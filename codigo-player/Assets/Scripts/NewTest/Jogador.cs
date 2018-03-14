using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jogador : MonoBehaviour {

		public int VIDA = 100;
		public int currentVida;

	//apagar
	public float EstaminaCheia = 100;
	public float EstaminaAtual;

		public string cena;

		void Awake () {
			transform.tag = "Player"; 
			currentVida = VIDA;	
		}
		
	void Update(){
		SistemaDeVida ();
	}
		void SistemaDeVida () {
		if (currentVida >= 100) {
			currentVida = 100;
		}
		else if (currentVida <= 0) {
			currentVida = 0;
			Death ();
			}	
		}

		void Death () {
			SceneManager.LoadScene (cena);		
		} 
		

}
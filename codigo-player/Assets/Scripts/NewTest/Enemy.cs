using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour {

		public float vida = 100;
		bool chamouMorte = false;
		private bool podeAtacar;
		private NavMeshAgent navMesh;
		private GameObject player;
		
	void Awake () {
		podeAtacar = true;
		player = GameObject.FindWithTag ("Player");
		navMesh = GetComponent<NavMeshAgent> ();

	}

		void Update () {
			if (vida <= 0) {
				vida = 0;
				if (chamouMorte == false) {
					chamouMorte = true;
					StartCoroutine ("Morrer");
				}
			}
			
				navMesh.destination = player.transform.position;
				if (Vector3.Distance (transform.position, player.transform.position) < 1.5f) {
					Atacar ();	
				}
		}

		IEnumerator Morrer(){
			//GetComponent<MeshRenderer> ().material.color = Color.red;
			yield return new WaitForSeconds (0);
			Destroy (gameObject);
		}

	void Atacar(){
		if (podeAtacar == true) {
			StartCoroutine ("TempoDeAtaque");
			player.GetComponent<Jogador> ().VIDA -= 20;
		}

	}

	IEnumerator TempoDeAtaque(){
		podeAtacar = false;
		yield return new WaitForSeconds (1);
		podeAtacar = true;
	} 
}

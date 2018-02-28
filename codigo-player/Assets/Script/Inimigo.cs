using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Inimigo : MonoBehaviour {

	private GameObject player;
	private NavMeshAgent navMesh;
	private bool podeAtacar;

	void Awake () {
		podeAtacar = true;
		player = GameObject.FindWithTag ("Player");
		navMesh = GetComponent<NavMeshAgent> ();

	}


	void Update () {
		navMesh.destination = player.transform.position;
		if (Vector3.Distance (transform.position, player.transform.position) < 1.5f) {
			Atacar ();	
		}

	}

	void Atacar(){
		if (podeAtacar == true) {
			StartCoroutine ("TempoDeAtaque");
			player.GetComponent<Jogador> ().VIDA -= 40;
		}

	}
	IEnumerator TempoDeAtaque(){
		podeAtacar = false;
		yield return new WaitForSeconds (1);
		podeAtacar = true;
	}


}

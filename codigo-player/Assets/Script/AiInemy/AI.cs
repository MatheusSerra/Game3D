/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AI : MonoBehaviour {
	public Transform Player;
	private NavMeshAgent naveMesh;
	private float DistanciaDoPlayer, DistanciaDoAIPoint;
	public float DistanciaDePercepcao = 30,DistanciaDeSeguir = 20, DistanciaDeAtacar = 2, VelocidadeDePasseio = 3, VelocidadeDePerseguicao = 6,TempoPorAtaque = 1.5f, DanoDoInimigo = 40;
	private bool VendoOPlayer;
	public Transform[] DestinosAleatorios;
	private int AIPointAtual; 
	private bool PerseguindoAlgo, contadorPerseguindoAlgo,atacandoAlgo;
	private float cronometroDaPerseguicao,cronometroAtaque;
	void Start (){
		AIPointAtual = Random.Range (0, DestinosAleatorios.Length);
		naveMesh = transform.GetComponent<NavMeshAgent> ();
	}
	void Update (){
		DistanciaDoPlayer = Vector3.Distance(Player.transform.position,transform.position);
		DistanciaDoAIPoint =  Vector3.Distance(DestinosAleatorios[AIPointAtual].transform.position,transform.position);
		//============================== RAYCAST ===================================//
		RaycastHit hit;
		Vector3 deOnde = transform.position;
		Vector3 paraOnde = Player.transform.position;
		Vector3 direction = paraOnde - deOnde;
		if(Physics.Raycast (transform.position,direction,out hit,1000) && DistanciaDoPlayer < DistanciaDePercepcao ){
			if(hit.collider.gameObject.CompareTag("Player")){
				VendoOPlayer = true;
			}else{
				VendoOPlayer = false;
			}
		}
		//================ CHECHAGENS E DECISOES DO INIMIGO ================//
		if(DistanciaDoPlayer > DistanciaDePercepcao){
			Passear();
		}
		if (DistanciaDoPlayer <= DistanciaDePercepcao && DistanciaDoPlayer > DistanciaDeSeguir) {
			if(VendoOPlayer == true){
				Olhar ();
			}else{
				Passear();
			}
		}
		if (DistanciaDoPlayer <= DistanciaDeSeguir && DistanciaDoPlayer > DistanciaDeAtacar) {
			if(VendoOPlayer == true){
				Perseguir();
				PerseguindoAlgo = true;
			}else{
				Passear();
			}
		}
		if (DistanciaDoPlayer <= DistanciaDeAtacar) {
			Atacar();
		}
		//COMANDOS DE PASSEAR
		if (DistanciaDoAIPoint <= 2) {
			AIPointAtual = Random.Range (0, DestinosAleatorios.Length);
			Passear();
		}
		//CONTADORES DE PERSEGUICAO
		if (contadorPerseguindoAlgo == true) {
			cronometroDaPerseguicao += Time.deltaTime;
		}
		if (cronometroDaPerseguicao >= 5 && VendoOPlayer == false) {
			contadorPerseguindoAlgo = false;
			cronometroDaPerseguicao = 0;
			PerseguindoAlgo = false;
		}
		// CONTADOR DE ATAQUE
		if (atacandoAlgo == true) {
			cronometroAtaque += Time.deltaTime;
		}
		if (cronometroAtaque >= TempoPorAtaque && DistanciaDoPlayer <= DistanciaDeAtacar) {
			atacandoAlgo = true;
			cronometroAtaque = 0;
			PLAYER.VIDA = PLAYER.VIDA - DanoDoInimigo;
			Debug.Log ("recebeuAtaque");
		} else if (cronometroAtaque >= TempoPorAtaque && DistanciaDoPlayer > DistanciaDeAtacar) {
			atacandoAlgo = false;
			cronometroAtaque = 0;
			Debug.Log ("errou");
		}
	}
	void Passear (){
		if (PerseguindoAlgo == false) {
			naveMesh.acceleration = 5;
			naveMesh.speed = VelocidadeDePasseio;
			naveMesh.destination = DestinosAleatorios [AIPointAtual].position;
		} else if (PerseguindoAlgo == true) {
			contadorPerseguindoAlgo = true;
		}
	}
	void Olhar(){
		naveMesh.speed = 0;
		transform.LookAt (Player);
	}
	void Perseguir(){
		naveMesh.acceleration = 8;
		naveMesh.speed = VelocidadeDePerseguicao;
		naveMesh.destination = Player.position;
	}
	void Atacar (){
		atacandoAlgo = true;
	}
}
*/
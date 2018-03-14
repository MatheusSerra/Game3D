using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class Controle : MonoBehaviour {
	private CharacterController controlador;
	private GameObject Jogador;
	private float UltimaPosicaoEmY,DistanciaDeQueda;
	[Range(1,15)]
	public float AlturaQueda = 4,DanoPorMetro = 5 ;
	public Image BarraVida, BarraEstamina, BarraFome, BarraSede;
	[Range(20,500)]
	public float VidaCheia = 100, EstaminaCheia = 100, FomeCheia = 100, SedeCheia = 100,velocidadeEstamina = 250;
	[HideInInspector]
	public float VidaAtual, EstaminaAtual, FomeAtual, SedeAtual;
	private bool semEstamina = false;
	private float cronometroFome,cronometroSede,velocidadeCaminhando,velocidadeCorrendo;
	public string cena;


	void Start (){
		controlador = GetComponent <CharacterController> ();
		VidaAtual = VidaCheia;
		EstaminaAtual = EstaminaCheia;
		FomeAtual = FomeCheia;
		SedeAtual = SedeCheia;
		Jogador = GameObject.FindWithTag ("Player");
		velocidadeCaminhando = Jogador.GetComponent<FirstPersonController> ().m_WalkSpeed;
		velocidadeCorrendo = Jogador.GetComponent<FirstPersonController> ().m_RunSpeed;
	}
	void Update (){
//		SistemaDeQueda ();
		SistemaDeVida ();
		SistemaDeEstamina ();
	//	SistemaDeFome ();
		//SistemaDeSede ();
		AplicarBarras ();
	}
	/*void SistemaDeQueda(){
		if (UltimaPosicaoEmY > Jogador.transform.position.y && controlador.velocity.y < 0) {
			DistanciaDeQueda += UltimaPosicaoEmY-Jogador.transform.position.y;
		}
		UltimaPosicaoEmY = Jogador.transform.position.y;
		if (DistanciaDeQueda >= AlturaQueda && controlador.isGrounded) {
			VidaAtual = VidaAtual - DanoPorMetro*DistanciaDeQueda;
			DistanciaDeQueda = 0;
			UltimaPosicaoEmY = 0;
		}
		if (DistanciaDeQueda < AlturaQueda && controlador.isGrounded) {
			DistanciaDeQueda = 0;
			UltimaPosicaoEmY = 0;
		}
	}	*/
	/*void SistemaDeFome(){
		FomeAtual -= Time.deltaTime;
		if (FomeAtual >= FomeCheia) {
			FomeAtual = FomeCheia;
		}
		if (FomeAtual <= 0) {
			FomeAtual = 0;
			cronometroFome += Time.deltaTime;
			if (cronometroFome >= 3) {
				VidaAtual -= (VidaCheia * 0.005f);
				EstaminaAtual -= (EstaminaCheia * 0.1f);
				cronometroFome = 0;
			}
		} else {
			cronometroFome = 0;
		}
	}
	void SistemaDeSede(){
		SedeAtual -= Time.deltaTime;
		if (SedeAtual >= SedeCheia) {
			SedeAtual = SedeCheia;
		}
		if (SedeAtual <= 0) {
			SedeAtual = 0;
			cronometroSede += Time.deltaTime;
			if (cronometroSede >= 3) {
				EstaminaAtual -= (EstaminaCheia * 0.1f);
				cronometroSede = 0;
			}
		} else {
			cronometroSede = 0;
		}
	} */
	void SistemaDeEstamina(){
		float multEuler = ((1/EstaminaCheia) * EstaminaAtual)*((1/FomeCheia) * FomeAtual);
		if (EstaminaAtual >= EstaminaCheia) {
			EstaminaAtual = EstaminaCheia;
		} else { 
			EstaminaAtual += Time.deltaTime*(velocidadeEstamina/40)*Mathf.Pow(2.718f,multEuler);
		}
		if (EstaminaAtual <= 0) {
			EstaminaAtual = 0;
			Jogador.GetComponent<FirstPersonController> ().m_RunSpeed = velocidadeCaminhando;
			semEstamina = true;
		}
		if (semEstamina == true && EstaminaAtual >= (EstaminaCheia * 0.15f)) {
			Jogador.GetComponent<FirstPersonController> ().m_RunSpeed = velocidadeCorrendo;
			semEstamina = false;
		}
		if (Input.GetKey (KeyCode.LeftShift) && semEstamina == false) {
			EstaminaAtual -= Time.deltaTime*(velocidadeEstamina/15)*Mathf.Pow(2.718f,multEuler);
		}
	}
	void SistemaDeVida(){
		if (VidaAtual >= VidaCheia) {
			VidaAtual = VidaCheia;
		} else if (VidaAtual <= 0) {
			VidaAtual = 0;
		Death();
		}
	}

	void Death (){
			SceneManager.LoadScene (cena);
	}

	void AplicarBarras(){
		BarraVida.fillAmount = ((1/VidaCheia) * VidaAtual);
		BarraEstamina.fillAmount = ((1/EstaminaCheia) * EstaminaAtual);
	//	BarraFome.fillAmount = ((1/FomeCheia) * FomeAtual);
	//	BarraSede.fillAmount = ((1/SedeCheia) * SedeAtual);
	}
	
}
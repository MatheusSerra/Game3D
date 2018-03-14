using UnityEngine;
using System.Collections;

public class Coletaveis : MonoBehaviour{
	public enum TIPO{
		Comida,
		Bebida,
		Vida,
		Energia
	}
	public TIPO TipoDoItem;
	[Range(1,500)]
	public float QuantoRepor = 50;
	private GameObject Jogador;
	void Start (){
		Jogador = GameObject.FindWithTag ("Player");
	}
	void OnTriggerStay(Collider other){
		if (Input.GetKeyDown ("e") && other.gameObject == Jogador.gameObject) {
			switch (TipoDoItem) {
			case TIPO.Comida:
				Jogador.GetComponent<Controle> ().FomeAtual += QuantoRepor;
				Destroy (gameObject);
				break;
			case TIPO.Bebida:
				Jogador.GetComponent<Controle> ().SedeAtual += QuantoRepor;
				Destroy (gameObject);
				break;
			case TIPO.Vida:
				Jogador.GetComponent<Controle> ().VidaAtual += QuantoRepor;
				Destroy (gameObject);
				break;
			case TIPO.Energia:
				Jogador.GetComponent<Controle> ().EstaminaAtual += QuantoRepor;
				Destroy (gameObject);
				break;
			}
		}
	}
}
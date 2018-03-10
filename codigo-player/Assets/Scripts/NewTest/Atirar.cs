using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

[Serializable]
public class LaserOuMira{
	public bool ativarLaser = false;
	public Color corLaser = Color.red;
	public bool AtivarMiraComum = true;
}

[Serializable]
public class Arma{
	[HideInInspector]
	public int balasExtra, balasNoPente;
	//
	public int danoPorTiro = 40;
	[Range(2,100)]
	public int numeroDeBalas = 240;
	[Range(1,50)]
	public int balasPorPente = 30;
	[Range(0.01f,5.0f)]
	public float tempoPorTiro = 0.3f;
	[Range(0.01f,5.0f)]
	public float tempoDaRecarga = 0.5f;
	[Space(10)]
	public LaserOuMira Miras;
	[Space(10)]
	public GameObject objetoArma;
	public GameObject lugarParticula;
	public GameObject particulaFogo;
	public AudioClip somTiro, somRecarga;
}
[RequireComponent(typeof(AudioSource))]
public class Atirar : MonoBehaviour {

	public KeyCode botaoRecarregar = KeyCode.R;
	public int armaInicial = 0;
	public string TagInimigo = "inimigo";
	public Text BalasPente, BalasExtra;
	public Material MaterialLasers;
	public Arma[] armas;
	//
	int armaAtual;
	AudioSource emissorSom;
	bool recarregando, atirando;
	LineRenderer linhaDoLaser;
	GameObject luzColisao;

	void Start () {
		//laser das armas
		luzColisao = new GameObject ();
		luzColisao.AddComponent<Light> ();
		luzColisao.GetComponent<Light> ().intensity = 8;
		luzColisao.GetComponent<Light> ().bounceIntensity = 8;
		luzColisao.GetComponent<Light> ().range = 0.2f;
		luzColisao.GetComponent<Light> ().color = Color.red;
		LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer> ();
		lineRenderer.material = MaterialLasers;
		lineRenderer.SetColors (Color.white, Color.white);
		lineRenderer.SetWidth (0.015f, 0.05f);
		lineRenderer.SetVertexCount (2);
		linhaDoLaser = GetComponent<LineRenderer> ();
		//
		for (int x = 0; x < armas.Length; x++) {
			armas [x].objetoArma.SetActive (false);
			armas [x].lugarParticula.SetActive (false);
			armas [x].balasExtra = armas [x].numeroDeBalas - armas [x].balasPorPente;
			armas [x].balasNoPente = armas [x].balasPorPente;
			armas [x].Miras.corLaser.a = 1;
		}
		if (armaInicial > armas.Length-1) {
			armaInicial = armas.Length-1;
		}
		armas [armaInicial].objetoArma.SetActive (true);
		armas [armaInicial].lugarParticula.SetActive (true);
		armaAtual = armaInicial;
		emissorSom = GetComponent<AudioSource> ();
		recarregando = atirando = false;
	}

	void Update () {
		//UI
		BalasExtra.text = "BalasExtra: " + armas[armaAtual].balasExtra;
		BalasPente.text = "BalasNoPente: " + armas[armaAtual].balasNoPente;
		//troca de armas
		if (Mathf.Abs (Input.GetAxis ("Mouse ScrollWheel")) > 0 && recarregando == false && atirando == false) {
			if(Input.GetAxis ("Mouse ScrollWheel") > 0){
				armaAtual++;
			}
			if(Input.GetAxis ("Mouse ScrollWheel") < 0){
				armaAtual--;
			}
			if (armaAtual < 0) {
				armaAtual = armas.Length - 1;
			}
			if (armaAtual > armas.Length - 1) {
				armaAtual = 0;
			}
			AtivarArmaAtual ();
		}
		//atirar
		if (Input.GetMouseButtonDown (0) && armas[armaAtual].balasNoPente > 0 && recarregando == false && atirando == false) {
			atirando = true;
			StartCoroutine (TempoTiro (armas [armaAtual].tempoPorTiro));
			emissorSom.clip = armas [armaAtual].somTiro;
			emissorSom.PlayOneShot (emissorSom.clip);
			armas [armaAtual].balasNoPente--;
			GameObject balaTemp = Instantiate (armas [armaAtual].particulaFogo, armas [armaAtual].lugarParticula.transform.position, transform.rotation) as GameObject;
			Destroy (balaTemp, 0.5f);
			//
			RaycastHit pontoDeColisao;
			if (Physics.Raycast (transform.position, transform.forward, out pontoDeColisao)) {
				if (pontoDeColisao.transform.gameObject.tag == TagInimigo) {
					pontoDeColisao.transform.gameObject.GetComponent<Enemy> ().vida -= armas[armaAtual].danoPorTiro;
				}
			}
		}
		//recarregar
		if (Input.GetKeyDown (botaoRecarregar) && recarregando == false && atirando == false && (armas[armaAtual].balasNoPente < armas[armaAtual].balasPorPente) && (armas [armaAtual].balasExtra > 0)) {
			emissorSom.clip = armas [armaAtual].somRecarga;
			emissorSom.PlayOneShot (emissorSom.clip);
			int todasAsBalas = armas [armaAtual].balasNoPente + armas [armaAtual].balasExtra;
			if (todasAsBalas >= armas [armaAtual].balasPorPente) {
				armas [armaAtual].balasNoPente = armas [armaAtual].balasPorPente;
				armas [armaAtual].balasExtra = todasAsBalas - armas [armaAtual].balasPorPente;
			} else {
				armas [armaAtual].balasNoPente = todasAsBalas;
				armas [armaAtual].balasExtra = 0;
			}
			recarregando = true;
			StartCoroutine (TempoRecarga(armas[armaAtual].tempoDaRecarga));
		}
		//laser da arma
		if (recarregando == false) {
			if (armas [armaAtual].Miras.ativarLaser == true) {
				linhaDoLaser.enabled = true;
				linhaDoLaser.material.SetColor ("_TintColor", armas [armaAtual].Miras.corLaser);
				luzColisao.SetActive (true);
				Vector3 PontoFinalDoLaser = transform.position + (transform.forward * 500);
				RaycastHit hitDoLaser;
				if (Physics.Raycast (transform.position, transform.forward, out hitDoLaser, 500)) {
					linhaDoLaser.SetPosition (0, armas [armaAtual].lugarParticula.transform.position);
					linhaDoLaser.SetPosition (1, hitDoLaser.point);
					float distancia = Vector3.Distance (transform.position, hitDoLaser.point) - 0.03f;
					luzColisao.transform.position = transform.position + transform.forward * distancia;
				} else {
					linhaDoLaser.SetPosition (0, armas [armaAtual].lugarParticula.transform.position);
					linhaDoLaser.SetPosition (1, PontoFinalDoLaser);
					luzColisao.transform.position = PontoFinalDoLaser;
				}
			}
		} else {
			linhaDoLaser.enabled = false;
			luzColisao.SetActive (false);
		}
		//checar limites da municao
		if (armas [armaAtual].balasNoPente > armas [armaAtual].balasPorPente) {
			armas [armaAtual].balasNoPente = armas [armaAtual].balasPorPente;
		}else if (armas [armaAtual].balasNoPente < 0) {
			armas [armaAtual].balasNoPente = 0;
		}
		int numBalasExtra = armas [armaAtual].numeroDeBalas - armas [armaAtual].balasPorPente;
		if (armas [armaAtual].balasExtra > numBalasExtra) {
			armas [armaAtual].balasExtra = numBalasExtra;
		}else if (armas [armaAtual].balasExtra < 0) {
			armas [armaAtual].balasExtra = 0;
		}
	}

	IEnumerator TempoTiro(float tempoDoTiro){
		yield return new WaitForSeconds (tempoDoTiro);
		atirando = false;
	}

	IEnumerator TempoRecarga(float tempoAEsperar){
		yield return new WaitForSeconds (tempoAEsperar);
		recarregando = false;
	}

	void AtivarArmaAtual(){
		for (int x = 0; x < armas.Length; x++) {
			armas [x].objetoArma.SetActive (false);
			armas [x].lugarParticula.SetActive (false);
		}
		armas [armaAtual].objetoArma.SetActive (true);
		armas [armaAtual].lugarParticula.SetActive (true);
		if (armas [armaAtual].Miras.ativarLaser == true) {
			linhaDoLaser.material.color = armas [armaAtual].Miras.corLaser;
			linhaDoLaser.enabled = true;
			luzColisao.SetActive (true);
			luzColisao.GetComponent<Light> ().color = armas [armaAtual].Miras.corLaser;
		} else {
			linhaDoLaser.enabled = false;
			luzColisao.SetActive (false);
		}
	}

	void OnGUI(){
		if (armas [armaAtual].Miras.AtivarMiraComum == true) {
			GUIStyle stylez = new GUIStyle();
			stylez.alignment = TextAnchor.MiddleCenter;
			GUI.skin.label.fontSize = 20;
			GUI.Label (new Rect (Screen.width / 2-6, Screen.height / 2-12, 12, 22), "+");
		}
	}
}
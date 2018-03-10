using UnityEngine;
using System.Collections;

public class Municao : MonoBehaviour {

	public int numeroDeBalas = 30, numeroDaArma = 0;

	public KeyCode TeclaPegarMunicao = KeyCode.E;
	public AudioClip SomPegarMunicao;
	public float distanciaDoItem = 3;
	public Atirar ScriptAtirar;
	bool podePegarOItem;
	AudioSource emissorSom;

	void Start(){
		emissorSom = GetComponent<AudioSource> ();
	}

	void Update () {
		float distancia = Vector3.Distance (transform.position, ScriptAtirar.gameObject.transform.position);
		if (distancia < distanciaDoItem) {
			podePegarOItem = true;
		} else {
			podePegarOItem = false;
		}
		if (Input.GetKeyDown (TeclaPegarMunicao) && podePegarOItem) {
			int numBalasExtra = ScriptAtirar.armas [numeroDaArma].numeroDeBalas - ScriptAtirar.armas [numeroDaArma].balasPorPente;
			if (ScriptAtirar.armas [numeroDaArma].balasExtra < numBalasExtra) {
				ScriptAtirar.armas [numeroDaArma].balasExtra += numeroDeBalas;
				GameObject emissorSom = new GameObject ();
				emissorSom.AddComponent (typeof(AudioSource));
				emissorSom.GetComponent<AudioSource> ().PlayOneShot (SomPegarMunicao);
				Destroy (emissorSom.gameObject, 5);
				Destroy (gameObject);
			}
		}
	}

	void OnGUI(){
		if (podePegarOItem == true) {
			GUIStyle stylez = new GUIStyle ();
			stylez.alignment = TextAnchor.MiddleCenter;
			GUI.skin.label.fontSize = 20;
			GUI.Label (new Rect (Screen.width / 2 - 50, Screen.height / 2 + 50, 200, 30), "Pressione: " + TeclaPegarMunicao);
		}
	}
}
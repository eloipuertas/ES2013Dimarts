using UnityEngine;
using System.Collections;

public class ExitDoorTrigger : MonoBehaviour {
	
	//Aqui hay que poner el numero de la escena que toque
	const int nivel2 = 5;
	const int nivel1 = 2;

	//Este es el flag que indica que se puede pasar por la puerta
	public bool nivel_completado;
	
	//Instanciacion de otros scripts
	public HUD hud;
	
	//Variables
	int score;
	
	// Use this for initialization
	void Start () {
		
		nivel_completado = false;
		PlayerPrefs.SetInt("Nivel",nivel1);//Si el jugador empieza el nivel 1, se sobreescribe la partida.
	
	}
	
	// Update is called once per frame
	void Update () {
		if(nivel_completado){

			this.collider.isTrigger = true;
			
		}	
	}
	
	public void OnTriggerEnter (Collider Player) {
		if (Player.collider.tag == "Player") {       
	    	Debug.Log ("Juego Terminado");
			PlayerPrefs.SetInt("Nivel",nivel2);
			PlayerPrefs.SetInt("ScoreNivel1",hud.getCurrentTotalScore());
			Application.LoadLevel(nivel2);
		}
	}
	
	public void setNivel_Completado(bool final){
		
		if(!nivel_completado){
			animation.CrossFade("Obrir");
		}
		this.nivel_completado = final;
	}
}

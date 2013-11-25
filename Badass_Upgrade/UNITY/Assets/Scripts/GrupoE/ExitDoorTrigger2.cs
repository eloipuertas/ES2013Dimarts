using UnityEngine;
using System.Collections;

public class ExitDoorTrigger2 : MonoBehaviour {
	
	//Aqui hay que poner el numero de la escena que toque
	const int victoria = 3;
	const int nivel2 = 2;

	//Este es el flag que indica que se puede pasar por la puerta
	public bool nivel_completado;
	
	//Instanciacion de otros scripts
	//public HUD hud;
	
	// Use this for initialization
	void Start () {
		
		nivel_completado = false;
	
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
			//PlayerPrefs.SetInt("ScoreNivel2",hud.getCurrentTotalScore());
			Application.LoadLevel(victoria);
		}
	}
	
	public void setNivel_Completado(bool final){
		
		if(!nivel_completado){
			animation.CrossFade("Obrir");
		}
		this.nivel_completado = final;
	}
}

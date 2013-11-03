using UnityEngine;
using System.Collections;

public class ExitDoorTrigger : MonoBehaviour {
	
	const int game_over = 3;

	
	bool nivel_completado;
	
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
			Application.LoadLevel(game_over);
		}
	}
	
	public void setNivel_Completado(bool final){
		this.nivel_completado = final;
	}
}

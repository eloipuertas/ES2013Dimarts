using UnityEngine;
using System.Collections;

public class ExitDoorTrigger : MonoBehaviour {
	
	//Aqui hay que poner el numero de la escena que toque
	const int nivel2 = 3;

	//Este es el flag que indica que se puede pasar por la puerta
	public bool nivel_completado;
	
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
			Application.LoadLevel(nivel2);
		}
	}
	
	public void setNivel_Completado(bool final){
		this.nivel_completado = final;
	}
}

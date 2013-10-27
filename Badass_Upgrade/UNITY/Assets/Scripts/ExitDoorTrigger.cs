using UnityEngine;
using System.Collections;

public class ExitDoorTrigger : MonoBehaviour {
	
//	public EnemiesAmount enemiesCounter;
//	private int numberOfEnemies;

	// Use this for initialization
	bool nivel_completado = false;
	const int game_over = 3;
	void Start () {
		
//		numberOfEnemies = enemiesCounter.numOfEnem;
	
	}
	
	// Update is called once per frame
	void Update () {
		if(nivel_completado){

			this.collider.isTrigger = true;
			
			
		}
//		numberOfEnemies = enemiesCounter.numOfEnem;
//		if(numberOfEnemies<=0){
//		// BoxCollider
//			collider.isTrigger = true;
//		}	
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

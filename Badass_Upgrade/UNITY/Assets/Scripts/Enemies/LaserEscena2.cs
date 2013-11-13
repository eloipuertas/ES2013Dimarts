using UnityEngine;
using System.Collections;

public class LaserEscena2 : MonoBehaviour {
    private GameObject player;
    private GameObject laser;
	private GameObject cam;
	private GameObject enemy;
        
        
        void Awake(){
            player = GameObject.FindGameObjectWithTag("Player");
            laser = GameObject.FindGameObjectWithTag("Laser");
			cam=GameObject.FindGameObjectWithTag("MainCamera");
			enemy= GameObject.FindGameObjectWithTag("Enemy");
		
        }
        
	
        void OnTriggerStay(Collider other){
            if(other.gameObject == player){
				Debug.Log("LASER ACTIVAT");
				cam.SendMessage("engega_llums");
				enemy.SendMessage("activar");
				laser.SetActive(false);
                        
            }
        }



}
using UnityEngine;
using System.Collections;

public class LaserEscena2 : MonoBehaviour {
    private GameObject player;
    private GameObject laser;
	private GameObject cam;
        
        
        void Awake(){
            player = GameObject.FindGameObjectWithTag("Player");
            laser = GameObject.FindGameObjectWithTag("Laser");
			cam=GameObject.FindGameObjectWithTag("MainCamera");
		
        }
        
	
        void OnTriggerStay(Collider other){
            if(other.gameObject == player){
                laser.SetActive(false);
				cam.SendMessage("engega_llums");
				cam.SendMessage("activa_enemics");
                        
            }
        }



}
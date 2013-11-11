using UnityEngine;
using System.Collections;

public class LaserPlayerDetection : MonoBehaviour {

	private GameObject player;
	private GameObject laser;
	
	
	void Awake(){
		player = GameObject.FindGameObjectWithTag("Player");
		laser = GameObject.FindGameObjectWithTag("Laser");
	}
	
	void OnTriggerStay(Collider other){
		if(other.gameObject == player){
			// start shooting tha player!!!
			laser.SetActive(false);
			Debug.Log("Activo la torreta que comença a fotre gardela ");
		}
	}
}

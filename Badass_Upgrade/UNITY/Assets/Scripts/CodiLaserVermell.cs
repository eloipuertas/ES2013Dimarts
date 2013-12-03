using UnityEngine;
using System.Collections;

public class CodiLaserVermell : MonoBehaviour {
	
	GameObject player;
	public int valorDany;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerStay(Collider other){
		Debug.Log("Dintre laser vermell");
		if(other.CompareTag("Player")){ 
	
        	player.gameObject.SendMessage("rebreAtac", valorDany);
        }
	}
}

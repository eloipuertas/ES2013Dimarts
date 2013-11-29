using UnityEngine;
using System.Collections;

public class CodiMunicio : MonoBehaviour {
	
	public GameObject player;
	
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	

	void OnTriggerEnter(Collider other) {
		if(other.gameObject==player){
			player.SendMessage("addItemMunicio",10);
	        Destroy(gameObject);
		}
	}
    

}

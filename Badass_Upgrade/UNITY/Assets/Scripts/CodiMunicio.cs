using UnityEngine;
using System.Collections;

public class CodiMunicio : MonoBehaviour {
	
	public GameObject player;
	int posWeapon;
	public int municio = 10;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		if(other.gameObject==player){
			if(this.gameObject.tag == "municioPistola") {
				posWeapon = 0;
			}
			else if(this.gameObject.tag == "municioRifle") {
				posWeapon = 1;
			}
			int[] paramsMunicio = {municio, posWeapon};
			player.SendMessage("addItemMunicio",paramsMunicio);
	        Destroy(gameObject);
		}
	}
}

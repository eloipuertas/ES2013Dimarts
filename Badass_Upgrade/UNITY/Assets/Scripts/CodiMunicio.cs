using UnityEngine;
using System.Collections;

public class CodiMunicio : MonoBehaviour {
	
	public GameObject Municio;
	public GameObject Player;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	

	void OnTriggerEnter(Collider other) {
		Player.SendMessage("addItemMunicio",10);
        Destroy(gameObject);

	}
    

}

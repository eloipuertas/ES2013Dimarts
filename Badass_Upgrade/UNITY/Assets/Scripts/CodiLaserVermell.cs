using UnityEngine;
using System.Collections;

public class CodiLaserVermell : MonoBehaviour {
	
	public GameObject Player;
	public int valorDany;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerStay(Collider other){
		Debug.Log("Dintre laser vermell");
		if(other.CompareTag("Player")){ 
	
        	Player.gameObject.SendMessage("rebreAtac", valorDany);
        }
	}
}

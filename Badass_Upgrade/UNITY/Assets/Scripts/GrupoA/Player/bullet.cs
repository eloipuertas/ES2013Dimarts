using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {
	
	int damage;
	
	// Use this for initialization
	void Start () {
		Destroy(this.gameObject,5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other) {
		//Debug.Log("Toco con el bullet -> "+other.collider.gameObject.name);
		if(other.collider.gameObject.tag == "Enemy") {
			other.transform.gameObject.SendMessage("rebreDany",damage);
			//Debug.Log("Damage enemic = "+damage);
			Destroy(this.gameObject);
		}
		else if(other.collider.gameObject.tag == "Barril") {			
			//Debug.Log("TOCO AL BARRIL ->"+other.name);
		    other.transform.gameObject.SendMessage("rebreTir");
		}		
		else if(other.collider.gameObject.tag != "Enemy" && (other.collider.gameObject.tag != "Player") && (other.collider.gameObject.tag != "muzzleFlash")) {
			Destroy(this.gameObject);
		}
		
    }
	
	void addDamage(int damage) {
		this.damage = damage;
	}
}

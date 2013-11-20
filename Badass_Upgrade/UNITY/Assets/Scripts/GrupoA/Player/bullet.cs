using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {
	
	int damage;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnTriggerEnter(Collider other) {
		Debug.Log("Toco "+other.collider.gameObject.tag);
		if(other.collider.gameObject.tag == "Enemy") {
			Debug.Log("Toco l'eneimc");
			other.transform.gameObject.SendMessage("rebreDany",damage);
			Debug.Log("Li faic dany a l'eneimic = "+damage);
			Destroy(this.gameObject);
		}
		else if(other.collider.gameObject.tag != "Enemy" && (other.collider.gameObject.tag != "Player")) {
			Destroy(this.gameObject);
		}
		//Si no toca re, al cap de 5 segons desapareix
		Destroy(this.gameObject,5f);
    }
	
	void addDamage(int damage) {
		this.damage = damage;
	}
}

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
		if(other.collider.gameObject.tag == "Enemy") {
			Debug.Log("Toco l'eneimc");
			other.transform.gameObject.SendMessage("rebreDany",damage);
			Debug.Log("Li faic dany a l'eneimic = "+damage);
		}
		//Destroy(this.gameObject);
    }
	
	void addDamage(int damage) {
		this.damage = damage;
	}
}

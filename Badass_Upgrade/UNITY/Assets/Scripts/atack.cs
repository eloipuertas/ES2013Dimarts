using UnityEngine;
using System.Collections;

public class atack : MonoBehaviour {
	
	public GameObject target;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
        if(other.CompareTag("targetCube")){ 
			Debug.Log("Juannnn l'hispa");;
		}
    }
}

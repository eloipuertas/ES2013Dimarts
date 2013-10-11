using UnityEngine;
using System.Collections;

public class atack : MonoBehaviour {
	
	RaycastHit hit;
	float hitDistance = 1.8f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Melee")) {
			if(Physics.Raycast(transform.position,transform.forward,out hit,hitDistance)) {
				if(hit.collider.gameObject.tag == "targetCube") {
					Debug.Log("Toco el cub");
				}
			}
		}
		
		
	}
}

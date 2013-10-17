using UnityEngine;
using System.Collections;

public class atack : MonoBehaviour {
	
	RaycastHit hit;
	float meleeDistance = 1.8f;
	Transform cam; 
	
	// Use this for initialization
	void Start () {
		cam = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Melee")) {
			if(Physics.Raycast(cam.position, cam.forward,out hit, meleeDistance)) {
				if(hit.collider.gameObject.tag == "Enemy") {
					Debug.Log("toco l'enemic amb un atac melee");
				}
			}
		}
		
		
	}
}

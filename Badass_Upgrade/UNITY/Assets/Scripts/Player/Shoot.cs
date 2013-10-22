using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
	
	RaycastHit hit;
	float shotDistance = 20f;
	Transform cam; 
	// Use this for initialization
	void Start () {
		cam = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetButtonDown("Disparar")) {
			if(Physics.Raycast(cam.position, cam.forward,out hit, shotDistance)) {
				if(hit.collider.gameObject.tag == "Enemy") {
					Debug.Log("Disparo i toco l'enemic");
					//Agafar de l'arma que estic el mal que fa i enviarl-la al enemic
				}
			}
			
		}
	
	}
}

/*
 * 
 * //Una altre manera per disparar
 * Transform cam = Camera.main.transform;
			Ray r = new Ray(cam.position,cam.forward);
			if(Physics.Raycast(r, out hit, 50f)) {
				if(hit.collider.gameObject.tag == "Enemy") {
					Debug.Log("Disparo i toco l'enemic");
				}
			}
 * 
 * 
 * 
 * 
 * */
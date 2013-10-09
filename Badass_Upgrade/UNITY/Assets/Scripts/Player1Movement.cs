using UnityEngine;
using System.Collections;

public class Player1Movement : MonoBehaviour {
	
	float moveSpeed = 100.0f;
	
	
	
	// Use this for initialization
	void Start () {
		
		 if (rigidbody==null)

         gameObject.AddComponent ("Rigidbody");

 

      // don't let the physics engine rotate the character

      rigidbody.freezeRotation = true;
	
	}
	
	// Update is called once per frame
	void Update () {
		
		
		//Walk
		float moveForward = Input.GetAxis("Vertical") * moveSpeed * Time.smoothDeltaTime;	
		float moveLeft = Input.GetAxis("Horizontal") * moveSpeed * Time.smoothDeltaTime;
		//float rotate = Input.GetAxis("Horizontal") * moveSpeed * Time.smoothDeltaTime;
		
		transform.Translate(Vector3.forward * moveForward);
		transform.Translate(Vector3.right * moveLeft);
		//transform.Translate(Vector3.up * rotate);
		
		//Jump
		if (Input.GetKeyDown ("space")) {
                 transform.Translate(Vector3.up * 260 * Time.deltaTime, Space.World);
		} 
		
		//Run
		/*if(Input.GetAxis("Run")> 0.0) {
			moveSpeed = 1000.0f;
		}
		else {
			moveSpeed = 100.0f;
		}*/
	        		
	}
}


/*
 * 
 * Run with a tab
 * if (Input.GetKeyDown ("tab")) { 
                 moveSpeed = 1000.0f;
		}
		else if(Input.GetKeyUp ("tab")) {
			moveSpeed = 100.0f;
		}*/
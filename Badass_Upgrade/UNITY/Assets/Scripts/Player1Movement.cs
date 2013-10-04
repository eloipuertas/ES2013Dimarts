using UnityEngine;
using System.Collections;

public class Player1Movement : MonoBehaviour {
	
	float moveSpeed = 100.0f;
	
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		
		
		float moveForward = Input.GetAxis("Vertical") * moveSpeed * Time.smoothDeltaTime;	
		float moveLeft = Input.GetAxis("Horizontal") * moveSpeed * Time.smoothDeltaTime;
		//float rotate = Input.GetAxis("Horizontal") * moveSpeed * Time.smoothDeltaTime;
		
		transform.Translate(Vector3.forward * moveForward);
		transform.Translate(Vector3.right * moveLeft);
		//transform.Translate(Vector3.up * rotate);
		if (Input.GetKeyDown ("space"))
                 transform.Translate(Vector3.up * 260 * Time.deltaTime, Space.World);
	        		
	}
}

/*

  
 
      */ 
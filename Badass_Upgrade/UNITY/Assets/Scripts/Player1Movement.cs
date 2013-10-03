using UnityEngine;
using System.Collections;

public class Player1Movement : MonoBehaviour {
	
	float xAxisValue;
	float zAxisValue;
	
	float moveSpeed = 60.0f;
	
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
		
	        		
	}
}

/*

  
  xAxisValue = Input.GetAxis("Horizontal") * moveSpeed;
	    zAxisValue = Input.GetAxis("Vertical") * moveSpeed;
		
	    if(Camera.current != null) {
			Camera.current.transform.Translate(new Vector3(xAxisValue, 0.0f, zAxisValue));
		}
      */ 
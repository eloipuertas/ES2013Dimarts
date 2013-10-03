using UnityEngine;
using System.Collections;

public class Player1Movement : MonoBehaviour {
	
	float walkAcceleration = 5;
	public GameObject cameraPlayer;
	float jump = 20;
	Vector2 horizontalMove;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		//transform.rotation = Quaternion.Euler(0,cameraPlayer.GetComponent(Player1Mouse).currentYRotation,0);
		
		rigidbody.AddRelativeForce(Input.GetAxis("Horizontal")*walkAcceleration,0,Input.GetAxis("Vertical")*walkAcceleration);
	
	}
}

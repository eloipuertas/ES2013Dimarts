using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	
	public Transform target;					//Target position
	private Transform _myTransform; 			//Camera position 
	public string playerTagName = "Player";		//Player tag
	public float walkDistance = 30.0f;			//Distance between target and camera when walking
	public float runDistance = 6.0f;			//Distance between the camera and the character when is running
	public float height = 2.0f; 				//Height of the camera
	public float heightAutomaticCamera = 20.0f;	//Height of the camera when playing automatic camera mode
	public float xSpeed = 250.0f;
	public float ySpeed = 120.0f;
	public float heightDamping = 2.0f; 			//delay of the camera returning. Entre mas bajo mas delay
	public float rotationDamping = 3.0f; 		// dealy of the camera returning. Entre mas bajo mas delay	
	
	private float _x;
	private float _y;
	

	//First called function. Main assignations
	void Awake(){
		//Initializing the camera position
		_myTransform = transform;		
	}
	
	
	// Use this for initialization
	void Start () {
		
		if(target==null){ //Checking if we have a target
			Debug.LogWarning("We do not have a target for the camera.");	
		}
		else{
			//Set up the camera
			CameraSetUp();													
		}		
	}

	// Update is called once per frame
	public void CameraSetUp(){
		//Look at the target position and place behind
		_myTransform.position = new Vector3(target.position.x,target.position.y + height,target.position.z - walkDistance);
		//Camera look at the player
		_myTransform.LookAt(target);
	
	}
	
	//Function used is LateUpdate instead Update (Reason: Camera must follow an object and normally every object is moved in the Update() function, so the camera will have the final position of that object in every frame)
	void LateUpdate(){
		
		//If a target is setted to the camera in order to follow it 
		if(target != null){
					
			//Catch the x and y axis values
			_x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
			_y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
						
			RotateCamera();   			 
		}
		//If any target is setted to the camera in order to follow it
		else{
			GameObject go = GameObject.FindGameObjectWithTag(playerTagName);
			
			//If we dont find anything
			if(go == null){
				return;
			}
			
			//If we find something
			target = go.transform;
		}
	}
	
	
	private void RotateCamera(){
		
		Quaternion rotation = Quaternion.Euler(_y, _x, 0);
    	Vector3 position = rotation * new Vector3(0.0f, 0.0f + height, -walkDistance) + target.position; 
    	_myTransform.rotation = rotation;
    	_myTransform.position = position;
	}
}

using UnityEngine;
using System.Collections;

public class Player1Mouse : MonoBehaviour {
	
	float mouseSensitivity = 2.5f;
	float xRotation;
	float yRotation;
	
	float minimumY = 0F;
	float maximumY = 90F;
	
	public float currentXRotation;
	public float currentYRotation;
	private float yRotationV;
	float xRotationV;
	//Temps que tartda en arribar de xRot a currentXRot...
	float moveTime = 0.05f;
	

	// Use this for initialization
	void Start () {
		
		//mouse not visibility in screen
		Screen.lockCursor = true;
		Screen.showCursor = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		yRotation += Input.GetAxis("Mouse X") * mouseSensitivity;
		xRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		
		//pos now, new pos,velocity, time to move
		currentYRotation = Mathf.SmoothDamp(currentYRotation,yRotation,ref yRotationV,moveTime);
		currentXRotation = Mathf.SmoothDamp(currentXRotation,xRotation,ref xRotationV,moveTime);
				
		
		//Final rotation
		transform.rotation = Quaternion.Euler(currentXRotation,currentYRotation,0);
	
	}
}

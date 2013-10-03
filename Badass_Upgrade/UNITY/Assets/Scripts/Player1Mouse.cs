using UnityEngine;
using System.Collections;

public class Player1Mouse : MonoBehaviour {
	
	float mouseSensitivity = 5.0f;
	float xRotation;
	float yRotation;
	
	public float currentXRotation;
	public float currentYRotation;
	private float yRotationV = 0.0F;
	float xRotationV;
	//Temps que tartda en arribar de xRot a currentXRot...
	float moveTime = 0.1f;
	

	// Use this for initialization
	void Start () {
		
		//mouse not visibility in screen
		Screen.lockCursor = true;
	
	}
	
	// Update is called once per frame
	void Update () {
		
		yRotation += Input.GetAxis("Mouse X") * mouseSensitivity;
		xRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		
		
		//Trobar error, es per suavitzar el mouse
		//pos now, new pos,velocity, time to move
		currentYRotation = Mathf.SmoothDamp(currentYRotation,yRotation,ref yRotationV,moveTime);
		currentXRotation = Mathf.SmoothDamp(currentXRotation,xRotation,ref xRotationV,moveTime);
		
		
		//Final rotation
		transform.rotation = Quaternion.Euler(currentXRotation,currentYRotation,0);
	
	}
}

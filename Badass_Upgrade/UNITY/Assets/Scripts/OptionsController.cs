using UnityEngine;
using System.Collections;

public class OptionsController : MonoBehaviour {
	
	//Constants
	const int main_menu = 0;
	
	//Variables
	public bool isBackToMainButton = false;
	
	// Use this for initialization
	void Start () {
		//Should the cursor be visible?
		Screen.showCursor = true;
		//The cursor will automatically be hidden, centered on view and made to never leave the view.
		Screen.lockCursor = false;	
	}
	
	//This function is called when the mouse entered the GUIElement or Collider
	public void OnMouseEnter(){
		renderer.material.color = Color.blue;
	}
	
	//This function is called when the mouse is not any longer over the GUIElement or Collider
	public void OnMouseExit(){
		renderer.material.color = Color.white;
	}
	
	//This function is called when the user has released the mouse button
	public void OnMouseUpAsButton(){

		if(isBackToMainButton)
			Application.LoadLevel(main_menu);
	}
}

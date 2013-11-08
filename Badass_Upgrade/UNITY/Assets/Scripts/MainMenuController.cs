using UnityEngine;
using System.Collections;
		
//MainMenuController.cs - This script will take control of the main menu options
		
public class MainMenuController : MonoBehaviour {
	
	//Constants
	const int new_game = 1;
	const int options = 2;
	
	//Variables
	public bool isQuitButton = false;			//Is the button the quit button?
	public bool isNewGameButton = false;
	
	// Use this for initialization
	void Start () {
		//Should the cursor be visible?
		Screen.showCursor = true;
		//The cursor will automatically be hidden, centered on view and made to never leave the view.
		Screen.lockCursor = false;	
	}

	//This function is called when the mouse entered the GUIElement or Collider
	/*public void OnMouseEnter(){
		renderer.material.color = Color.blue;
	}*/
	
	//This function is called when the mouse is not any longer over the GUIElement or Collider
	/*public void OnMouseExit(){
		renderer.material.color = Color.white;
	}*/
	
	//This function is called when the user has released the mouse button
	public void OnMouseUpAsButton(){

		if(isQuitButton)
			Application.Quit();
		else if(isNewGameButton)
			Application.LoadLevel(new_game);
		else
			Application.LoadLevel(options);
	}
}
using UnityEngine;
using System.Collections;
		
//MainMenuController.cs - This script will take control of the main menu options
		
public class MainMenuController : MonoBehaviour {
	
	//Constants
	const int new_game = 1;
	const int options = 2;
	const int credits = 5;
	
	//Variables
	public bool isQuitButton = false;			//Is the button the quit button?
	public bool isNewGameButton = false;
	public bool isCreditsButton = false;
	
	public Texture originalTexture;
	public Texture hoverTexture;
	public Vector3 scale;
	
	// Use this for initialization
	void Start () {
		//Should the cursor be visible?
		Screen.showCursor = true;
		//The cursor will automatically be hidden, centered on view and made to never leave the view.
		Screen.lockCursor = false;	
	}

	//This function is called when the mouse entered the GUIElement or Collider
	public void OnMouseEnter(){
		originalTexture = guiTexture.texture;
		scale = transform.localScale;
		
		if(isQuitButton){
			hoverTexture = Resources.Load("exit") as Texture;
			guiTexture.texture = hoverTexture;
			transform.localScale = new Vector3(0.01F, 0.01F, transform.localScale.z);
		}else if(isNewGameButton){
			hoverTexture = Resources.Load("newGame") as Texture;
			guiTexture.texture = hoverTexture;
			transform.localScale = new Vector3(0.01F, 0.01F, transform.localScale.z);
		}else if(isCreditsButton){
			hoverTexture = Resources.Load("creditos") as Texture;
			guiTexture.texture = hoverTexture;
			transform.localScale = new Vector3(0.01F, 0.01F, transform.localScale.z);
		}else{
			hoverTexture = Resources.Load("options") as Texture;
			guiTexture.texture = hoverTexture;
			transform.localScale = new Vector3(0.01F, 0.01F, transform.localScale.z);
		}
	}
	
	//This function is called when the mouse is not any longer over the GUIElement or Collider
	public void OnMouseExit(){
		guiTexture.texture = originalTexture;
		transform.localScale = scale;
	}
	
	//This function is called when the user has released the mouse button
	public void OnMouseUpAsButton(){

		if(isQuitButton)
			Application.Quit();
		else if(isNewGameButton)
			Application.LoadLevel(new_game);
		else if(isCreditsButton)
			Application.LoadLevel(credits);
		else
			Application.LoadLevel(options);
	}
}
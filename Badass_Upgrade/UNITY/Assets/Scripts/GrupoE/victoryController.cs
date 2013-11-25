using UnityEngine;
using System.Collections;

public class victoryController : MonoBehaviour {

	//Constants
	const int menu = 0;
	//Variables
	public bool isReturnMenu = false;
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
	
	// Update is called once per frame
	void Update () {
	}
	
	//This function is called when the mouse entered the GUIElement or Collider
	public void OnMouseEnter(){
		originalTexture = guiTexture.texture;
		scale = transform.localScale;
		
		if(isReturnMenu){
			hoverTexture = Resources.Load("return") as Texture;
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
		if(isReturnMenu)
			Application.LoadLevel(menu);
	}
}

using UnityEngine;
using System.Collections;

public class gameOverMenuController : MonoBehaviour {

	//Constants
	const int new_game = 1;
	const int espera = 10; // Tiempo de espera hasta reiniciar nivel
	
	//Variables
	public bool isNewGameButton = false;
	public TextMesh count;
	//public GameObject ob;
	//public TextMesh mesh;

	
	// Use this for initialization
	void Start () {
		//Should the cursor be visible?
		Screen.showCursor = true;
		//The cursor will automatically be hidden, centered on view and made to never leave the view.
		Screen.lockCursor = false;
		// Muestra la cuenta atras
		//ob = (GameObject)Instantiate(Resources.Load("Texto"));
		//mesh = (TextMesh) ob.GetComponent("TextMesh");
		//mesh.text = ""+espera;
		count.text = ""+espera;
	}
	
	private void Update()
	{
		if(Time.timeSinceLevelLoad > espera)
			Application.LoadLevel(new_game);
		else
			count.text = ""+(10 - (int)Time.timeSinceLevelLoad);
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
		if(isNewGameButton)
			Application.LoadLevel(new_game);
	}
}

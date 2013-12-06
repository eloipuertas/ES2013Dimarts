using UnityEngine;
using System.Collections;

public class gameOverMenuController : MonoBehaviour {

	//Constants
	const int new_game = 1;
	const int espera = 10; // Tiempo de espera hasta reiniciar nivel
	
	//Variables
	public bool isNewGameButton = false;
	public GUIText count;
	//public GameObject ob;
	//public TextMesh mesh;
	public Texture originalTexture;
	public Texture hoverTexture;
	public Vector3 scale;

	
	// Use this for initialization
	void Start () {
		Time.timeScale=1;
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
		//if(Input.anyKey)
		//	Application.LoadLevel(new_game);
		if(Time.timeSinceLevelLoad > espera)
			Application.LoadLevel(new_game);
		else
			count.text = ""+(10 - (int)Time.timeSinceLevelLoad);
	}

	//This function is called when the mouse entered the GUIElement or Collider
	public void OnMouseEnter(){
		originalTexture = guiTexture.texture;
		scale = transform.localScale;
		
		if(isNewGameButton){
			hoverTexture = Resources.Load("restartGame") as Texture;
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
		if(isNewGameButton)
			Application.LoadLevel(new_game);
	}
}

using UnityEngine;
using System.Collections;
 
public class pauseGame : MonoBehaviour
{
	const int main_menu = 0;
	const int new_game = 1;
	
	private GUISkin mySkin;
	private Rect windowRect;
	private bool paused = false;
	private MouseLook cameraML;
	private GameObject player;
	private MouseLook playerML;
	
	private void Start()
	{
		windowRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200);
		cameraML = (MouseLook)GetComponent("MouseLook");
		player = GameObject.Find("Player");
		playerML = (MouseLook)player.GetComponent("MouseLook");
	}
	
	private void Update()
	{
		if(Input.GetButtonDown("Escape") || Input.GetKey(KeyCode.P))
		{
			paused = !paused;
		}
		
		if(paused)
		{	
			Time.timeScale = 0;
			AudioListener.pause = true;
			cameraML.enabled = false;
			playerML.enabled = false;
			//Screen.lockCursor = false;
		}
		else{
			Time.timeScale = 1;
			AudioListener.pause = false;
			cameraML.enabled = true;
			playerML.enabled = true;
			//Screen.lockCursor = true;
			
		}
	}
	
	private void OnGUI()
	{
		if (mySkin != null) {
	        GUI.skin = mySkin;
	    }
		if(paused)
			windowRect = GUI.Window (0,windowRect,windowFunc, "Menu de Pause");
	}
	
	private void windowFunc(int id)
	{
		if(GUILayout.Button("Resume"))
		{
			paused = false;
		}
		
		if(GUILayout.Button("Reiniciar nivel"))
		{
			Application.LoadLevel(new_game);
		}
		
		if(GUILayout.Button("Salir al menu principal"))
		{
			Application.LoadLevel(main_menu);
		}
		
		if(GUILayout.Button("Salir a Windows"))
		{
			Application.Quit();
		}
	}
}
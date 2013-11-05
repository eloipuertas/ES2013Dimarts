using UnityEngine;
using System.Collections;
 
public class controlGUI : MonoBehaviour
{
	const int main_menu = 0;
	const int new_game = 1;
	const int mov = 1; //Movimiento
	const int cam = 2; //Camara
	const int act = 3; //Acciones
	const int menu = 4; //Acciones
	
	private GUISkin pauseMenu;
	private GUISkin tips;
	private Rect windowRect;
	private bool paused;
	public int showTip;
	public MouseLook cameraML;
	public MouseLook playerML;
	
	private void Start()
	{
		paused = false;
		showTip = 0;
	}
	
	private void Update()
	{
		if(Input.GetButtonDown("Escape"))
		{
			paused = !paused;
		}
		
		if(paused)
		{	
			Time.timeScale = 0;
			AudioListener.pause = true;
			cameraML.enabled = false;
			playerML.enabled = false;
			Screen.showCursor=true;
			Screen.lockCursor = false; //Queremos usar el cursor cuando se muestra el menu.
		}
		else{
			Time.timeScale = 1;
			AudioListener.pause = false;
			cameraML.enabled = true;
			playerML.enabled = true;
			Screen.showCursor=false;
			Screen.lockCursor = true; //No queremos ver el cursor durante la partida.
			
			
			//SHOW TIPS
			int start = 4; // El tiempo que pasa entre cada mensaje de ayuda (en segundos).
			int stop = 10; // El tiempo que se muestra cada mensaje de ayuda (en segundos).
			if(Time.timeSinceLevelLoad > start && Time.timeSinceLevelLoad < (start + stop)){
				showTip = 1;
			}else if(Time.timeSinceLevelLoad > (2*start + stop) && Time.timeSinceLevelLoad < (2*start + 2*stop)){
				showTip = 2;
			}else if(Time.timeSinceLevelLoad > (3*start + 2*stop) && Time.timeSinceLevelLoad < (3*start + 3*stop)){
				showTip = 3;
			}else if(Time.timeSinceLevelLoad > (4*start + 3*stop) && Time.timeSinceLevelLoad < (4*start + 4*stop)){
				showTip = 4;
			}else{
				showTip = 0;
			}
		}
	}
	
	private void OnGUI()
	{
		if(paused){
			GUI.skin = pauseMenu;
			windowRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200);
			windowRect = GUI.Window (0,windowRect,windowPaused, "Menu de Pause");
		}else{
			GUI.skin = tips;
			switch(showTip){
				case mov:
					windowRect = new Rect( 5, 5, 400, 70);
					windowRect = GUI.Window (0,windowRect,windowTips, "Movimiento");
					break;
				case cam:
					windowRect = new Rect( 5, 5, 320, 120);
					windowRect = GUI.Window (0,windowRect,windowTips, "Camara");
					break;
				case act:
					windowRect = new Rect( 5, 5, 340, 140);
					windowRect = GUI.Window (0,windowRect,windowTips, "Acciones");
					break;
				case menu:
					windowRect = new Rect( 5, 5, 340, 50);
					windowRect = GUI.Window (0,windowRect,windowTips, "Menu de pause");
					break;
				
			}
		}
		
	}
	
	private void windowPaused(int id)
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
		
		if(GUILayout.Button("Salir al Escritorio"))
		{
			Application.Quit();
		}
	}
	
	private void windowTips(int id)
	{
		switch(showTip){
				case mov:
					GUILayout.Label("- Usa las teclas 'W' y 'S' para avanzar y retroceder.");
					GUILayout.Label("- Usa las teclas 'A' y 'D' para moverte a izquierda y derecha.");
					break;
				case cam:
					GUILayout.Label("- Mueve el mouse adelante y atras para girar la camara arriba y abajo.");
					GUILayout.Label("- Mueve el mouse a izquierda y derecha para girar la camara hacia los lados.");
					break;
				case act:
					GUILayout.Label("- Pulsa el boton MOUSE1 para disparar.");
					GUILayout.Label("- Pulsa el boton MOUSE2 para el ataque de Melee.");
					GUILayout.Label("- Pulsa la tecla 'R' para recargar el arma.");
					GUILayout.Label("- Pulsa la barra espaciadora para saltar.");
					break;
				case menu:
					GUILayout.Label("- Pulsa la tecla 'ESC' para abrir el menu de pause.");
					break;
				
			}
	}
}
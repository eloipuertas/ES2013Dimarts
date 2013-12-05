using UnityEngine;
using System.Collections;
 
public class controlGUI : MonoBehaviour
{
	const int main_menu = 0;
	const int new_game = 1;
	const int movement = 1; //Movimiento
	const int camera = 2; //Camara
	const int attack = 3; //Camara
	const int weapons = 4; //Camara
	const int actions = 5; //Acciones
	const int menu = 6; //Menu
	
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
			paused = true;
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
			Screen.showCursor = false;
			Screen.lockCursor = true; //No queremos ver el cursor durante la partida.
			
			
			//SHOW TIPS
			int start = 4; // El tiempo que pasa entre cada mensaje de ayuda (en segundos).
			int stop = 12; // El tiempo que se muestra cada mensaje de ayuda (en segundos).
			if(Time.timeSinceLevelLoad > start && Time.timeSinceLevelLoad < (start + stop)){
				showTip = 1;
			}else if(Time.timeSinceLevelLoad > (2*start + stop) && Time.timeSinceLevelLoad < (2*start + 2*stop)){
				showTip = 2;
			}else if(Time.timeSinceLevelLoad > (3*start + 2*stop) && Time.timeSinceLevelLoad < (3*start + 3*stop)){
				showTip = 3;
			}else if(Time.timeSinceLevelLoad > (4*start + 3*stop) && Time.timeSinceLevelLoad < (4*start + 4*stop)){
				showTip = 4;
			}else if(Time.timeSinceLevelLoad > (5*start + 4*stop) && Time.timeSinceLevelLoad < (5*start + 5*stop)){
				showTip = 5;
			}else if(Time.timeSinceLevelLoad > (6*start + 5*stop) && Time.timeSinceLevelLoad < (6*start + 6*stop)){
				showTip = 6;
			}else{
				showTip = 0;
			}
		}
	}
	
	private void OnGUI()
	{
		if(paused){
			GUI.skin = pauseMenu;
			windowRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 130);
			windowRect = GUI.Window (0,windowRect,windowPaused, "Menu de Pause");
		}else{
			GUI.skin = tips;
			switch(showTip){
				case movement:
					windowRect = new Rect( 5, 5, 400, 75);
					windowRect = GUI.Window (0,windowRect,windowTips, "Movimiento");
					break;
				case camera:
					windowRect = new Rect( 5, 5, 320, 110);
					windowRect = GUI.Window (0,windowRect,windowTips, "Camara");
					break;
				case attack:
					windowRect = new Rect( 5, 5, 340, 110);
					windowRect = GUI.Window (0,windowRect,windowTips, "Ataque");
					break;
				case weapons:
					windowRect = new Rect( 5, 5, 340, 110);
					windowRect = GUI.Window (0,windowRect,windowTips, "Armas");
					break;
				case actions:
					windowRect = new Rect( 5, 5, 340, 120);
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
		if(GUILayout.Button("Continuar"))
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
		
		if(GUILayout.Button("Salir del juego"))
		{
			Application.Quit();
		}
	}
	
	private void windowTips(int id)
	{
		switch(showTip){
				case movement:
					GUILayout.Label("- Usa las teclas 'W' y 'S' para avanzar y retroceder.");
					GUILayout.Label("- Usa las teclas 'A' y 'D' para moverte a izquierda y derecha.");
					break;
				case camera:
					GUILayout.Label("- Mueve el Mouse adelante y atras para girar la camara arriba y abajo.");
					GUILayout.Label("- Mueve el Mouse a izquierda y derecha para girar la camara hacia los lados.");
					break;
				case attack:
					GUILayout.Label("- Pulsa el boton MOUSE1 para disparar.");
					GUILayout.Label("- Pulsa el boton MOUSE2 para el ataque de Melee.");
					GUILayout.Label("- Pulsa la tecla 'R' para recargar el arma.");
					break;
				case weapons:
					GUILayout.Label("- Pulsa la tecla '1' para elegir la Pistola.");
					GUILayout.Label("- Pulsa la tecla '2' para elegir el Rifle.");
					break;
				case actions:
					GUILayout.Label("- Pulsa la tecla 'F' para encender y apagar la linterna.");
					GUILayout.Label("- Pulsa la tecla 'E' para interactuar con el entorno.");
					GUILayout.Label("- Pulsa la tecla 'CTRL' para agacharte.");
					GUILayout.Label("- Pulsa la barra espaciadora para saltar.");
					break;
				case menu:
					GUILayout.Label("- Pulsa la tecla 'ESC' para abrir el menu de pause.");
					break;
				
			}
	}
}
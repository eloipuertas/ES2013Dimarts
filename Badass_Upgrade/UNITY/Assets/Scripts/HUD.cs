using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {
	
	const int game_over = 3;
	
	//Contendra todos de enemigos en la escena
	GameObject[] enem;
	
	//El bloque que impide el paso en la salida
	GameObject portal;
	
	//Numero de enemigos en la escena
	int numOfEnem;
	
	/* Elementos de texto del HUD
	 * 
	 * vidaText: un objeto de tipo GUIText, muestra la vida
	 * escudoText: un objeto de tipo GUIText, muestra el escudo
	 * balasCargadorText: un objeto de tipo GUIText, muestra las balas del cargador
	 * balasTotalesText: un objeto de tipo GUIText, muestra las balas totales
	 * contadorEnemigos: un objeto de tipo GUIText, muestra el numero de enemigos
	 */
	public GUIText vidaText;
	public GUIText escudoText;
	public GUIText balasCargadorText;
	public GUIText balasTotalesText;
	public GUIText contadorEnemigos;

	/* Inicializacion de scripts externos
	 * 
	 * public MainCharacter robotProtagonista: El script que contiene los valores de interes para el HUD
	 */
	public MainCharacter robotProtagonista;

	
	//The first method to be called
	void Awake(){
				
	}
	
	// Use this for initialization
	void Start () {
		
		enem = GameObject.FindGameObjectsWithTag("Enemy");
		numOfEnem = enem.Length;
		contadorEnemigos.text="Enemies :"+numOfEnem;
		
		portal = GameObject.FindGameObjectWithTag("porta1");

		vidaText.text = robotProtagonista.vida.ToString() + "%";
		escudoText.text = robotProtagonista.escudo.ToString() + "%";
		
		balasCargadorText.text = robotProtagonista.balesCarregador.ToString();
		balasTotalesText.text = robotProtagonista.balesTotalsArmaActual.ToString();
	
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//Carga el menu game over si la vida baja a 0.
		if(robotProtagonista.vida <= 0){
			Application.LoadLevel(game_over);
		}
		
		//Por cada frame, actualiza los valores

		vidaText.text = robotProtagonista.vida.ToString() + "%";
		escudoText.text = robotProtagonista.escudo.ToString() + "%";		
		
		balasCargadorText.text = robotProtagonista.balesCarregador.ToString();;
		balasTotalesText.text = robotProtagonista.balesTotalsArmaActual.ToString();

	}
	
	public void enemyDeath(){
		
		numOfEnem--;
		contadorEnemigos.text="Enemies :"+numOfEnem;
		
		if(numOfEnem<=0){
			
			portal.SendMessage("setNivel_Completado",true,SendMessageOptions.DontRequireReceiver);
			
		}	
	}
}

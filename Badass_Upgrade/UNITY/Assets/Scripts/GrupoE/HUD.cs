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
	
	//Slot de arma equipada
	int weaponPos;
	
	/* Elementos de texto del HUD
	 * 
	 * vidaText: un objeto de tipo GUIText, muestra la vida
	 * escudoText: un objeto de tipo GUIText, muestra el escudo
	 * balasCargadorText: un objeto de tipo GUIText, muestra las balas del cargador
	 * balasTotalesText: un objeto de tipo GUIText, muestra las balas totales
	 * contadorEnemigos: un objeto de tipo GUIText, muestra el numero de enemigos
	 * slotArma#: un objeto de tipo GUIText, muestra el slot del arma equipada y de las demas
	 */
	public GUIText vidaText;
	public GUIText escudoText;
	public GUIText balasCargadorText;
	public GUIText balasTotalesText;
	public GUIText contadorEnemigos;
	public GUIText slotArma1;
	public GUIText slotArma2;
	public GUIText slotArma3;
	public GUITexture healthLine;
	public GUITexture shieldLine;

	/* Inicializacion de scripts externos
	 * 
	 * public MainCharacter robotProtagonista: El script que contiene los valores de interes para el HUD
	 */
	public MainCharacter robotProtagonista;
	
	
	Rect healthWidth;
	Rect shieldWidth;
	
	//The first method to be called
	void Awake(){
				
	}
	
	// Use this for initialization
	void Start () {
		
		enem = GameObject.FindGameObjectsWithTag("Enemy");
		numOfEnem = enem.Length;
		contadorEnemigos.text=numOfEnem.ToString();
		
		portal = GameObject.FindGameObjectWithTag("porta1");

		vidaText.text = robotProtagonista.vida.ToString() + "%";
		escudoText.text = robotProtagonista.escudo.ToString() + "%";
		
		balasCargadorText.text = robotProtagonista.balesCarregador.ToString();
		balasTotalesText.text = robotProtagonista.balesTotalsArmaActual.ToString();
		
		slotArma1.text = "1";
		slotArma2.text = "2";
		slotArma3.text = "3";
		
		weaponPos = robotProtagonista.posWeapon;
		
		healthWidth = healthLine.pixelInset;
		shieldWidth = shieldLine.pixelInset;
	
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
		//Carga el menu game over si la vida baja a 0.
		if(robotProtagonista.vida <= 0){
			Application.LoadLevel(game_over);
		}
		
		//Por cada frame, actualiza los valores
		
		healthWidth.width = robotProtagonista.vida;
		healthLine.pixelInset = healthWidth;
		
		shieldWidth.width = robotProtagonista.escudo;
		shieldLine.pixelInset = shieldWidth;

		vidaText.text = robotProtagonista.vida.ToString();
		escudoText.text = robotProtagonista.escudo.ToString();		
		
		balasCargadorText.text = robotProtagonista.balesCarregador.ToString();;
		balasTotalesText.text = robotProtagonista.balesTotalsArmaActual.ToString();
		
		weaponPos = robotProtagonista.posWeapon;
		
		if(weaponPos == 0){
			slotArma1.fontSize = 20;
			
			slotArma2.fontSize = 10;
			
			slotArma3.fontSize = 10;
			
		}else if(weaponPos == 1){
			slotArma1.fontSize = 10;
			
			slotArma2.fontSize = 20;
			
			slotArma3.fontSize = 10;
			
		}else if(weaponPos == 2){
			slotArma1.fontSize = 10;
			
			slotArma2.fontSize = 10;
			
			slotArma3.fontSize = 20;
			
		}

	}
	
	//Cuando muere un enemigo, se actualiza el contador de enemigos
	public void enemyDeath(){
		
		numOfEnem--;
		contadorEnemigos.text=numOfEnem.ToString();
		
		if(numOfEnem<=0){
			
			portal.SendMessage("setNivel_Completado",true,SendMessageOptions.DontRequireReceiver);
			
		}	
	}
}
